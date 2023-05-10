using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace Infrastructure.Messenger.Models
{
    public class Contact:BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey(nameof(ContactType))]
        public int TypeId { get; set; }
        [JsonIgnore]
        public virtual ContactType ContactType { get; set; }
        [JsonIgnore]
        public virtual ICollection<ContactFeatures> ContactFeatures { get; set; }

        public Contact GetContact(ContactDto contact)
        {
            Name = contact.Name;
            TypeId = contact.TypeId;
            return this;
        }

        public ContactDto GetDto()
        {
            return new ContactDto(Name, TypeId);
        }

        public ContactReadDto GetReadDto()
        {
            return new ContactReadDto(Id,Name, TypeId);
        }
    }

    public record ContactDto(string Name,int TypeId);
    public record ContactReadDto(int Id, string Name,int TypeId);
}
