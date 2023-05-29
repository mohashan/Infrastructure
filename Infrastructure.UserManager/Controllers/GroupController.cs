using Infrastructure.BaseControllers;
using Infrastructure.BaseTools;
using Infrastructure.BaseUserManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.UserManager.Controllers
{
    public class GroupController : GenericController<Group,GroupCreateDto,GroupReadDto,GroupListDto>
    {
        public GroupController(UserManagerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
        }
        [HttpPost("{groupId}/[action]/{userId}")]
        public async Task<ActionResult> AddUserToGroup(int groupId,int userId)
        {
            Group group = await ctx.Set<Group>().FindAsync(groupId) ?? throw new ArgumentException(nameof(userId));
            await group.AddUserToGroupAsync(userId, ctx);
            return Ok(new StandardResponse(true, "", null));
        }

        [HttpDelete("{groupId}/[action]/{userId}")]
        public async Task<ActionResult> RemoveUserFromGroup(Guid groupId, Guid userId)
        {
            Group group = await ctx.Set<Group>().FindAsync(groupId) ?? throw new ArgumentException(nameof(userId));
            await group.RemoveUserFromGroupAsync(userId, ctx);
            return Ok(new StandardResponse(true, "", null));
        }
    }
}