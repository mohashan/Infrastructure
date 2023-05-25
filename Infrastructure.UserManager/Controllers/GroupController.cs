using Infrastructure.BaseControllers;
using Infrastructure.BaseTools;
using Infrastructure.BaseUserManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.UserManager.Controllers
{
    public class GroupController : GenericController<Group,GroupDto,GroupReadDto>
    {
        public GroupController(UserManagerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
        }
        [HttpPost("{groupId:int}/[action]/{userId:int}")]
        public async Task<ActionResult> AddUserToGroup(int groupId,int userId)
        {
            Group group = await ctx.Set<Group>().FindAsync(groupId) ?? throw new ArgumentException(nameof(userId));
            await group.AddUserToGroupAsync(userId, ctx);
            return Ok(new StandardResponse(true, "", null));
        }

        [HttpDelete("{groupId:int}/[action]/{userId:int}")]
        public async Task<ActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            Group group = await ctx.Set<Group>().FindAsync(groupId) ?? throw new ArgumentException(nameof(userId));
            await group.RemoveUserFromGroupAsync(userId, ctx);
            return Ok(new StandardResponse(true, "", null));
        }
    }
}