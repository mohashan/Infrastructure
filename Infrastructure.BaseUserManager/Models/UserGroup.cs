using Infrastructure.BaseDomain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.BaseUserManager.Models
{
    public class UserGroup : BaseEntity<UserGroup, UserGroupDto, UserGroupReadDto>
    {
        [ForeignKey(nameof(this.User))]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(Group))]
        public int GroupId { get; set; }
        public Group Group { get; set; }

    }

    public class UserGroupDto : BaseDto<UserGroup, UserGroupDto, UserGroupReadDto>
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }

    }
    public class UserGroupReadDto : BaseReadDto<UserGroup, UserGroupDto, UserGroupReadDto>
    {
        public int UsreId { get; set; }
        public string? UserName{ get; set; }

        public int GroupId { get; set; }
        public string? GroupName { get; set; }
    }
}
