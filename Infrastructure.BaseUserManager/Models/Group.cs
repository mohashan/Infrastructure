using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Infrastructure.BaseUserManager.Models
{
    public class Group : BaseEntity<Group, GroupCreateDto, GroupReadDto,GroupListDto>
    {
        public virtual ICollection<UserGroup>? UserGroups { get; set; }
    }

    class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
        }
    }

    public class GroupCreateDto : BaseCreateDto<Group, GroupCreateDto, GroupReadDto, GroupListDto>
    {

    }
    public class GroupReadDto : BaseReadDto<Group, GroupCreateDto, GroupReadDto, GroupListDto>
    {
        public int UserGroupCount { get; set; }
    }

    public class GroupListDto : BaseListDto<Group, GroupCreateDto, GroupReadDto, GroupListDto>
    {
    }
}
