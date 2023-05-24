using AutoMapper.Features;
using Infrastructure.BaseDomain;
using Infrastructure.BaseTools;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Text;
using System.Threading.Channels;

namespace Infrastructure.Messenger.Models
{
    public class Message : BaseEntity<Message, MessageDto, MessageReadDto>
    {
        [ForeignKey(nameof(Channel))]
        public int ChannelId { get; set; }
        public Channel Channel { get; set; } 
        [ForeignKey(nameof(Contact))]
        public int ContactId { get; set; }
        public Contact Contact { get; set; } 
        [ForeignKey(nameof(Template))]
        public int TemplateId { get; set; }
        public Template Template { get; set; } 
        public string? Parameters { get; set; }
        public MessageState State { get; set; }

        public string? Response { get; set; }

        public string SentText { get; set; } = string.Empty;

        public void FillSentText(string TemplateText, string ChannelBodyRequest, string recipient)
        {
            var body = CreateBodyByReplaceParametersInTemplate(TemplateText);
            SentText = ChannelBodyRequest.Replace("@text", body).Replace("@to", recipient);
        }

        public string CreateBodyByReplaceParametersInTemplate(string TemplateText)
        {
            StringBuilder MessageText = new StringBuilder(TemplateText);
            string[] parameters = Parameters?.Split('|') ?? new string[] { string.Empty };

            for (int i = 0; i < parameters.Length; i++)
            {
                MessageText.Replace($"@param{i}", parameters[i]);
            }

            return MessageText.ToString();
        }

        
    }
    public class MessageDto: BaseDto<Message, MessageDto, MessageReadDto>
    {
        public int ChannelId { get; set; }
        public int ContactId { get; set; }
        public int TemplateId { get; set; }
        public string? Parameters { get; set; }

    }
    public class MessageReadDto: BaseReadDto<Message, MessageDto, MessageReadDto>
    {
        public int ChannelId { get; set; }
        public string? ChannelTitle { get; set; }
        public int ContactId { get; set; }
        public string? ContactTitle { get; set; }
        public int TemplateId { get; set; }
        public string? TemplateTitle { get; set; }
        public string? Parameters { get; set; }
        public string? MessageState { get; set; }
        public string? Response { get; set; }

        public string SentText { get; set; } = string.Empty;
    }

    public enum MessageState
    {
        Accepted,
        Sent,
        Error,
    }
}
