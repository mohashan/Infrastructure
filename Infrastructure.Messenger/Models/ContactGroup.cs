namespace Infrastructure.Messenger.Models
{
    public class ContactGroup:BaseEntity<ContactGroup,ContactGroupDto,ContactGroupReadDto>
    {
        public virtual ICollection<Contact>? Contacts { get; set; }
    }

    public record ContactGroupDto(string? title) : BaseDto(title);
    public record ContactGroupReadDto(int Id, string? title,DateTime InsertDate) : BaseReadDto(Id, title ,InsertDate);
}
