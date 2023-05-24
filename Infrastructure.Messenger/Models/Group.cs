using Infrastructure.BaseDomain;

namespace Infrastructure.Messenger.Models
{
    public class Group:BaseEntity<Group,GroupDto,GroupReadDto>
    {
        public virtual ICollection<ContactGroup>? ContactGroups { get; set; }
    }

    public class GroupDto : BaseDto<Group, GroupDto, GroupReadDto>
    {

    }
    public class GroupReadDto : BaseReadDto<Group, GroupDto, GroupReadDto>
    {
        public int ContactGroupCount { get; set; }
    }

}
