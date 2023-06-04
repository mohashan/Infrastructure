using Infrastructure.BaseControllers;
using Infrastructure.BaseTools;
using Infrastructure.BaseUserManager.IRepository;
using Infrastructure.BaseUserManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.UserManager.Controllers
{
    public class GroupController : GenericController<Group,GroupCreateDto,GroupReadDto,GroupListDto>
    {
        private readonly IGroupRepositiry groupRepo;

        public GroupController(UserManagerDbContext ctx, AutoMapper.IConfigurationProvider cfg,IGroupRepositiry groupRepo) : base(ctx, cfg)
        {
            this.groupRepo = groupRepo;
        }
        [HttpPost("{groupId}/[action]/{userId}")]
        public async Task<ActionResult> AddUserToGroup(Guid groupId,Guid userId)
        {
            string Name = $"{groupId}:{userId}";
            return Ok(await groupRepo.AddUserToGroupAsync(groupId, userId, Name));
        }

        [HttpDelete("{groupId}/[action]/{userId}")]
        public async Task<ActionResult> RemoveUserFromGroup(Guid groupId, Guid userId)
        {
            await groupRepo.RemoveUserFromGroupAsync(groupId, userId);
            return Ok();
        }
    }
}