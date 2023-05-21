
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Messenger.Models
{
    public class ContactGroup : BaseEntity<ContactGroup, ContactGroupDto, ContactGroupReadDto>
    {
        [ForeignKey(nameof(this.Contact))]
        public int ContactId { get; set; }
        public Contact Contact { get; set; } 
        [ForeignKey(nameof(this.Group))]
        public int GroupId { get; set; }
        public Group Group { get; set; } 

    }

    public record ContactGroupDto(string? title, int ContactId, int? GroupId) : BaseDto(title);
    public record ContactGroupReadDto(int Id, string? title, int ContactId, int? GroupId, DateTime InsertDate) : BaseReadDto(Id, title, InsertDate);
}
