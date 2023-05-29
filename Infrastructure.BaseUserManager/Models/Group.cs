using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Infrastructure.BaseUserManager.Models
{
    public class Group : BaseEntity<Group, GroupCreateDto, GroupReadDto,GroupListDto>
    {
        public virtual ICollection<UserGroup>? UserGroups { get; set; }
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

    public static class GroupRepositiryHelper
    {
        public static async Task RemoveUserFromGroupAsync(this Group group, Guid UserId, ApplicationDbContext context)
        {
            UserGroup userGroup = await context.Set<UserGroup>().FirstOrDefaultAsync(c => c.UserId == UserId && c.GroupId == group.Id)
                ?? throw new ArgumentException("This user is not part of this group");

            userGroup.DeleteDate = DateTime.Now;
            userGroup.IsDeleted = true;
            context.Entry(userGroup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public static async Task AddUserToGroupAsync(this Group group, int UserId, ApplicationDbContext context)
        {
            User user = await context.Set<User>().FindAsync(UserId) ?? throw new ArgumentException(nameof(UserId));
            await group.AddUserToGroupAsync(user,context);
        }

        public static async Task AddUserToGroupAsync(this Group group, User user, ApplicationDbContext context)
        {
            if (await context.Set<UserGroup>().AnyAsync(c => c.UserId == user.Id && c.GroupId == group.Id))
                throw new Exception("User already is enrolled in this group");

            context.Set<UserGroup>().Add(new UserGroup
            {
                GroupId = group.Id,
                Name = user.Name,
                UserId = user.Id
            });
            await context.SaveChangesAsync();
        }
    }
}
