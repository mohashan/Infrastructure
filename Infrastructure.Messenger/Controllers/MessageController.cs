using AutoMapper;
using AutoMapper.Features;
using Infrastructure.BaseTools;
using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Infrastructure.Messenger.Controllers
{
    public class MessageController : GenericController<Message, MessageDto, MessageReadDto>
    {
        private readonly IHttpRequester httpClient;

        public MessageController(IHttpRequester httpClient, MessengerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
            this.httpClient = httpClient;
        }

        [HttpPost]
        public override async Task<ActionResult> Create([FromBody] MessageDto dto)
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
        public async Task<ActionResult> SendToGroup(int groupId, [FromBody] MessageDto dto)
        {
            if (dto == null || groupId == 0)
            {
                return BadRequest();
            }

            var contacts = ctx.Set<ContactGroup>().Where(c => c.GroupId == groupId).ToList();
            MessageDto messageDto;
            Message message;
            List<KeyValuePair<int, string>> Errors = new List<KeyValuePair<int, string>>();
            foreach (var item in contacts)
            {
                messageDto = new MessageDto
                {
                    Name = dto.Name ?? item.Name,
                    ChannelId = dto.ChannelId,
                    ContactId = item.ContactId,
                    TemplateId = dto.TemplateId,
                    Parameters = dto.Parameters
                };

                try
                {
                    message = await SendMessage(messageDto);
                }
                catch (Exception ex)
                {
                    Errors.Add(new KeyValuePair<int, string>(item.ContactId, ex.Message));
                }
            }

            return Ok(new StandardResponse<List<KeyValuePair<int,string>>>(Errors.Any()?false:true,$"Message sent to group with {Errors.Count} error(s)", Errors));
        }

        private async Task<Message> SendMessage(MessageDto dto)
        {

            var entity = dto.GetEntity(mapper);

            Template template = ctx.Set<Template>().Find(dto.TemplateId) ?? throw new Exception("Template is not defined");
            Channel channel = ctx.Set<Channel>().Find(dto.ChannelId) ?? throw new Exception("Channel is not defined");

            var recipient = (await ctx.Set<ContactFeature>().FirstOrDefaultAsync(c => c.ContactId == dto.ContactId && c.FeatureId == channel.FeatureId)) ??
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
