using AutoMapper;
using AutoMapper.Features;
using Infrastructure.BaseControllers;
using Infrastructure.BaseDataProvider.Repository;
using Infrastructure.BaseTools;
using Infrastructure.BaseUserManager.Models;
using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Infrastructure.Messenger.Controllers
{
    public class MessageController : GenericController<Message, MessageCreateDto, MessageReadDto,MessageListDto>
    {
        private readonly IHttpRequester httpClient;

        public MessageController(IHttpRequester httpClient, MessengerDbContext ctx
            , AutoMapper.IConfigurationProvider cfg
            , IBaseRepository<Message, MessageCreateDto, MessageReadDto, MessageListDto> repo) : base(ctx, cfg,repo)
        {
            this.httpClient = httpClient;
        }

        [HttpPost]
        public override async Task<ActionResult> Create([FromBody] MessageCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            Message message = null;
            try
            {
                message = await SendMessage(dto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Message sent Error(message id : {message?.Id}): {ex.Message}");
            }


            return CreatedAtAction(nameof(Details),
                new { id = message?.Id }, new StandardResponse<MessageReadDto>(true,"Message created and sent successfully", message?.GetReadDto(mapper)));
        }

        [HttpPost("[action]/{groupId:int}")]
        public async Task<ActionResult> SendToGroup(Guid groupId, [FromBody] MessageCreateDto dto)
        {
            if (dto == null || groupId == new Guid())
            {
                return BadRequest();
            }

            var contacts = ctx.Set<UserGroup>().Where(c => c.GroupId == groupId).ToList();
            MessageCreateDto messageDto;
            Message message;
            List<KeyValuePair<Guid, string>> Errors = new List<KeyValuePair<Guid, string>>();
            foreach (var item in contacts)
            {
                messageDto = new MessageCreateDto
                {
                    Name = dto.Name ?? item.Name,
                    ChannelId = dto.ChannelId,
                    UserId = item.UserId,
                    TemplateId = dto.TemplateId,
                    Parameters = dto.Parameters
                };

                try
                {
                    message = await SendMessage(messageDto);
                }
                catch (Exception ex)
                {
                    Errors.Add(new KeyValuePair<Guid, string>(item.UserId, ex.Message));
                }
            }

            return Ok(new StandardResponse<List<KeyValuePair<Guid,string>>>(Errors.Any()?false:true,$"Message sent to group with {Errors.Count} error(s)", Errors));
        }

        private async Task<Message> SendMessage(MessageCreateDto dto)
        {

            var entity = dto.GetEntity(mapper);

            Template template = ctx.Set<Template>().Find(dto.TemplateId) ?? throw new Exception("Template is not defined");
            Channel channel = ctx.Set<Channel>().Find(dto.ChannelId) ?? throw new Exception("Channel is not defined");

            var recipient = (await ctx.Set<UserFeature>().FirstOrDefaultAsync(c => c.UserId == dto.UserId && c.FeatureId == channel.FeatureId)) ??
                throw new Exception("Message recipient not found");

            entity.FillSentText(template.Body, channel.HttpRequestBody ?? string.Empty, recipient.Value);

            ctx.Set<Message>().Add(entity);
            await ctx.SaveChangesAsync();

            entity.Response = await httpClient.SendAsync(new Uri(channel.EndPoint), HttpMethod.Post, entity.SentText, channel.AuthorizationToken);

            entity.State = MessageState.Sent;

            ctx.Entry(entity).State = EntityState.Modified;

            await ctx.SaveChangesAsync();

            return entity;
        }
    }


}
