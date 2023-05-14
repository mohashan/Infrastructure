using AutoMapper.Features;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Channels;

namespace Infrastructure.Messenger.Models
{
    public class Message:BaseEntity<Message,MessageDto,MessageReadDto>
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

        public string SentText { get; set; }

        public void FillSentText(string TemplateText,string ChannelBodyRequest, string recipient)
        {
            var body = MessageContent(TemplateText);
            this.SentText = ChannelBodyRequest.Replace("@text", body).Replace("@to", recipient);
        }

        public string MessageContent(string TemplateText)
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
    public record MessageDto(string? title, int ChannelId, int ContactId, int TemplateId, string? Parameters, MessageState State,string? Response,string SentText) 
        : BaseDto(title);
    public record MessageReadDto(int Id,string? title, int ChannelId, int ContactId, int TemplateId, string? Parameters, MessageState State,DateTime InsertDate, string? Response, string SentText) 
        : BaseReadDto(Id, title, InsertDate);

    public enum MessageState
    {
        Accepted,
        Sent,
        Error,
    }
}
