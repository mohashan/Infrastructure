using Infrastructure.BaseControllers;
using Infrastructure.BaseDataProvider.Repository;
using Infrastructure.BaseTools;
using Infrastructure.BaseUserManager.IRepository;
using Infrastructure.BaseUserManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UserManager.Controllers
{
    public class UserController : GenericController<User,UserCreateDto,UserReadDto,UserListDto>
    {
        private readonly IUserRepositiry userRepo;

        public UserController(UserManagerDbContext ctx, AutoMapper.IConfigurationProvider cfg,IUserRepositiry userRepo) : base(ctx, cfg, userRepo)
        {
            this.userRepo = userRepo;
        }
        [HttpPost("{userId}/feature/{featureId}")]
        public async Task<ActionResult> SetUserFeature(Guid userId,Guid featureId, string featureValue)
        {
            return Ok(await userRepo.SetFeatureValueAsync(userId, featureId, featureValue));
        }

        [HttpGet("{userId}/[action]")]
        public async Task<ActionResult> GetAllFeatures(Guid userId,int pageSize = 10, int pageNum = 1)
        {
            return Ok(await userRepo.GetAllFeatures(userId).Skip(pageNum*pageSize).Take(pageSize).ToListAsync());
        }

        [HttpDelete("{userId}/[action]/{featureId}")]
        public async Task<ActionResult> RemoveUserFeature(Guid userId, Guid featureId)
        {
            return Ok(await userRepo.RemoveFeatureValueAsync(userId, featureId));
        }
    }
}