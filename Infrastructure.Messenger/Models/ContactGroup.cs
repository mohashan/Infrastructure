
using Infrastructure.BaseDomain;
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

    public class ContactGroupDto : BaseDto<ContactGroup, ContactGroupDto, ContactGroupReadDto>
    {
        public int ContactId { get; set; }

        public int GroupId { get; set; }

    }
    public class ContactGroupReadDto : BaseReadDto<ContactGroup, ContactGroupDto, ContactGroupReadDto>
    {
        public int ContactId { get; set; }
        public string? ContactTitle { get; set; }

        public int GroupId { get; set; }
        public string? GroupTitle { get; set; }
    }
}
