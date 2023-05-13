using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace Infrastructure.Messenger.Models
{
    public class Contact:BaseEntity<Contact,ContactDto,ContactReadDto>
    {

        [ForeignKey(nameof(ContactType))]
        public int TypeId { get; set; }
        [JsonIgnore]
        public virtual ContactType ContactType { get; set; }
        [JsonIgnore]
        public virtual ICollection<ContactFeatures>? ContactFeatures { get; set; }

        [ForeignKey(nameof(ContactGroup))]
        public int? ContactGroupId { get; set; }
        public virtual ContactGroup? ContactGroup { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }

    public record ContactDto(string? title,int TypeId,int? ContactGroupId):BaseDto(title);
    public record ContactReadDto(int Id, string? title, int TypeId, int? ContactGroupId, DateTime InsertDate) : BaseReadDto(Id, title, InsertDate);
}
