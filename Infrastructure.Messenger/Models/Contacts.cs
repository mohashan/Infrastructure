using AutoMapper;
using Infrastructure.BaseDomain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace Infrastructure.Messenger.Models
{
    public class Contact:BaseEntity<Contact,ContactDto,ContactReadDto>
    {

        [ForeignKey(nameof(ContactType))]
        public int ContactTypeId { get; set; }
        [JsonIgnore]
        public virtual ContactType ContactType { get; set; } 
        [JsonIgnore]
        public virtual ICollection<ContactFeature>? ContactFeatures { get; set; }

        public virtual ICollection<ContactGroup>? ContactGroup { get; set; }

        public virtual ICollection<Message>? Messages { get; set; }
    }

    public class ContactDto:BaseDto<Contact, ContactDto, ContactReadDto>
    {
        public int ContactTypeId { get; set; }
    }
    public class ContactReadDto : BaseReadDto<Contact, ContactDto, ContactReadDto>
    {
        public int ContactTypeId { get; set; }
        public string ContactTypeTitle { get; set; } = string.Empty;
    }
}
