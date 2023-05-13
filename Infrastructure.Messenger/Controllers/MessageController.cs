using AutoMapper;
using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Infrastructure.Messenger.Controllers
{
    public class MessageController : GenericController<Message, MessageDto, MessageReadDto>
    {

        public MessageController(MessengerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
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

                StringBuilder MessageText = new StringBuilder(template.Body);
                string[] parameters = dto.Parameters?.Split('|') ?? new string[] { string.Empty };
                var RequestBody = ctx.Channels.FirstOrDefault(c => c.Id == dto.ChannelId);
                for (int i = 0; i < parameters.Length; i++)
                {
                    MessageText.Replace($"@param{i}", parameters[i]);
                }
                HttpResponseMessage response = null;
                string result = string.Empty;
                try
                {
                    entity.SentText = channel.HttpRequestBody
                                .Replace("@text", MessageText.ToString())
                                .Replace("@to", feature.Value);
                    response = await client.PostAsync("",
                        new StringContent(entity.SentText, Encoding.UTF8));
                    if (response == null)
                    {
                        throw new Exception("Response is null");
                    }
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Status code Error : {await response.Content.ReadAsStringAsync()}");
                    }


                    entity.State = MessageState.Sent;

                    entity.Response = await response?.Content?.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    entity.State = MessageState.Error;
                    entity.Response = ex.Message;
                }
                finally
                {
                    if (response != null)
                        response.Dispose();
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
