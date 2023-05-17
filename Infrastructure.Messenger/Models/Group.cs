namespace Infrastructure.Messenger.Models
{
    public class Group:BaseEntity<Group,GroupDto,GroupReadDto>
    {
        public virtual ICollection<ContactGroup>? ContactGroups { get; set; }
    }

    public record GroupDto(string? title) : BaseDto(title);
    public record GroupReadDto(int Id, string? title,DateTime InsertDate) : BaseReadDto(Id, title ,InsertDate);
}
