using AutoMapper;
using AutoMapper.Features;
using Infrastructure.BaseTools;
using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.Mvc;
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
                return BadRequest();
            }

            Message? message = null;
            try
            {
                message = await SendMessage(dto);
            }
            catch (Exception ex)
            {
                if (message is not null)
                {
                    message.Response = ex.Message;
                    message.State = MessageState.Error;
                }
            }


            return CreatedAtAction(nameof(Details),
                new { id = message.Id }, message.GetReadDto(mapper));
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
                messageDto = new MessageDto(dto.title ?? item.Title, dto.ChannelId, item.ContactId, dto.TemplateId, dto.Parameters);
                try
                {
                    message = await SendMessage(messageDto);
                }
                catch (Exception ex)
                {
                    Errors.Add(new KeyValuePair<int, string>(item.ContactId, ex.Message));
                }
            }

            return Ok(new { errors = Errors });
        }

        private async Task<Message> SendMessage(MessageDto dto)
        {

            var entity = new Message().GetEntity(dto, mapper);

            //entity.State = MessageState.Accepted;

            Template template = ctx.Set<Template>().Find(dto.TemplateId);
            Channel channel = ctx.Set<Channel>().Find(dto.ChannelId);

            if (template == null)
            {
                throw new Exception("Template is not defined");
            }

            if (channel == null)
            {
                throw new Exception("Channel is not defined");
            }

            
            // var message = await ctx.Set<Message>().Include(c => c.Template).Include(c => c.Channel).FirstOrDefaultAsync(c => c.Id == entity.Id);
            //if (message == null)
            //{
            //    throw new Exception($"Failed to find message {entity}");
            //}

            var sendTo = (await ctx.Set<ContactFeature>().FirstOrDefaultAsync(c => c.ContactId == dto.ContactId && c.FeatureId == channel.FeatureId))?.Value;

            if (sendTo == null)
            {
                throw new Exception("Not Found Value to send message");
            }

            string result = string.Empty;

            entity.FillSentText(template.Body, channel.HttpRequestBody ?? string.Empty, sendTo);
            try
            {
                ctx.Set<Message>().Add(entity);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Create new Entity failed");
            }

            try
            {
                entity.Response = await httpClient.SendAsync(new Uri(channel.EndPoint), HttpMethod.Post, entity.SentText, channel.AuthorizationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("Send Request failed", ex);
            }

            entity.State = MessageState.Sent;

            ctx.Entry(entity).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Update new Entity failed");
            }

            return entity;
        }
    }


}
