using System.ComponentModel.DataAnnotations.Schema;

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
    }
    public record MessageDto(string? title, int ChannelId, int ContactId, int TemplateId, string? Parameters, MessageState State) : BaseDto(title);
    public record MessageReadDto(int Id,string? title, int ChannelId, int ContactId, int TemplateId, string? Parameters, MessageState State,DateTime InsertDate) : BaseReadDto(Id, title, InsertDate);

    public enum MessageState
    {
        Accepted,
        Sent,
        Error,
    }
}
