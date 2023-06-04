using AutoMapper;
using Infrastructure.BaseDataProvider.Repository;
using Infrastructure.BaseDomain;
using Infrastructure.BaseUserManager.IRepository;
using Infrastructure.BaseUserManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;

namespace Infrastructure.BaseUserManager.Repository
{
    public class GroupRepositiry : BaseRepository<Group,GroupCreateDto,GroupReadDto,GroupListDto>, IGroupRepositiry
    {

        public GroupRepositiry(ApplicationDbContext ctx,IMapper mapper):base(ctx,mapper)
        {
        }
        public async Task RemoveUserFromGroupAsync(Guid groupId, Guid UserId)
        {
            UserGroup userGroup = await context.Set<UserGroup>().FirstOrDefaultAsync(c => c.UserId == UserId && c.GroupId == groupId)
                ?? throw new ArgumentException("This user is not part of this group");

            userGroup.DeleteDate = DateTime.Now;
            userGroup.IsDeleted = true;
            context.Entry(userGroup).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Guid> AddUserToGroupAsync(Guid groupId, Guid UserId, string Name)
        {
            var prevUserGroup = await context.Set<UserGroup>().FirstOrDefaultAsync(c => c.UserId == UserId && c.GroupId == groupId);
            if (prevUserGroup is not null)
                return prevUserGroup.Id;

            UserGroup userGroup = new UserGroup
            {
                GroupId = groupId,
                Name = Name,
                UserId = UserId,
            };
            context.Set<UserGroup>().Add(userGroup);

            await context.SaveChangesAsync();
            return userGroup.Id;
        }
    }
}
