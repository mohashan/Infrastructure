using Infrastructure.BaseControllers;
using Infrastructure.BaseTools;
using Infrastructure.BaseUserManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.UserManager.Controllers
{
    public class UserController : GenericController<User,UserCreateDto,UserReadDto,UserListDto>
    {
        public UserController(UserManagerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
        }
        [HttpPost("{userId}/feature/{featureId}")]
        public async Task<ActionResult> SetUserFeature(Guid userId,Guid featureId, string featureValue)
        {
            User user = ctx.Set<User>().Find(userId) ?? throw new ArgumentException(nameof(userId));

            var result = await user.SetFeatureValue(featureId, featureValue,ctx);
            return Ok(new StandardResponse<KeyValuePair<string,string>>(true, "Value Set", result));
        }

        [HttpGet("{userId}/[action]")]
        public async Task<ActionResult> GetAllFeatures(Guid userId)
        {
            User user = ctx.Set<User>().Find(userId) ?? throw new ArgumentException(nameof(userId));

            var result = await user.GetAllFeatures(ctx);
            return Ok(new StandardResponse<List<KeyValuePair<Guid, string>>>(true, "Get All Feature Values", result));
        }

        [HttpDelete("{userId}/[action]/{featureId}")]
        public async Task<ActionResult> RemoveUserFeature(Guid userId, Guid featureId)
        {
            User user = ctx.Set<User>().Find(userId) ?? throw new ArgumentException(nameof(userId));

            var result = await user.RemoveFeatureValue(featureId,ctx);
            return Ok(new StandardResponse<bool>(true, $"Remove featureId {featureId} for user {userId}", result));
        }
    }
}