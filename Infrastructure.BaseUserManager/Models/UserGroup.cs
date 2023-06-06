using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.BaseUserManager.Models
{
    public class UserGroup : BaseEntity<UserGroup, UserGroupCreateDto, UserGroupReadDto,UserGroupListDto>
    {
        [ForeignKey(nameof(this.User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(Group))]
        public Guid GroupId { get; set; }
        public Group Group { get; set; }

    }

    class UserGroupEntityConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
        }
    }

    public class UserGroupCreateDto : BaseCreateDto<UserGroup, UserGroupCreateDto, UserGroupReadDto, UserGroupListDto>
    {
        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }

    }
    public class UserGroupReadDto : BaseReadDto<UserGroup, UserGroupCreateDto, UserGroupReadDto, UserGroupListDto>
    {
        public Guid UsreId { get; set; }
        public string? UserName{ get; set; }

        public Guid GroupId { get; set; }
        public string? GroupName { get; set; }
    }

    public class UserGroupListDto : BaseListDto<UserGroup, UserGroupCreateDto, UserGroupReadDto, UserGroupListDto>
    {
        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }

    }
}
