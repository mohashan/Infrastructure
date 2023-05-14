using AutoMapper;
using Infrastructure.BaseTools;
using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.Mvc;
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
            var baseEnt = new BaseEntity<Message, MessageDto, MessageReadDto>();
            var entity = baseEnt.GetEntity(dto, mapper);

            var channel = ctx.Set<Channel>().FirstOrDefault(c => c.Id == dto.ChannelId);
            if (channel == null)
            {
                return NotFound("Channel not found");
            }

            var template = ctx.Set<Template>().FirstOrDefault(t => t.Id == dto.TemplateId);
            if (template == null)
            {
                return NotFound("Template not found");
            }

            var feature = ctx.Set<ContactFeatures>().FirstOrDefault(c => c.FeatureId == channel.FeatureId && c.ContactId == dto.ContactId);
            if (feature == null)
            {
                return NotFound("Contact feature not found");
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(channel.EndPoint);
                
                string result = string.Empty;
                try
                {
                    entity.FillSentText(template.Body, channel.HttpRequestBody??string.Empty ,feature.Value); ;

                    entity.Response = await httpClient.SendAsync(new Uri(channel.EndPoint), HttpMethod.Post, entity.SentText,channel.AuthorizationToken);
                    
                    entity.State = MessageState.Sent;
                }
                catch (Exception ex)
                {
                    entity.Response = ex.Message;
                    entity.State = MessageState.Error;
                }

            }
            try
            {
                ctx.Set<Message>().Add(entity);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Create new Entity failed");
            }



            return CreatedAtAction(nameof(Details),
                new { id = entity.Id }, entity.GetReadDto(mapper));
        }
    }
}
