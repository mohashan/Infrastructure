using Infrastructure.BaseControllers;
using Infrastructure.BaseTools;
using Infrastructure.BaseUserManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.UserManager.Controllers
{
    public class UserController : GenericController<User,UserDto,UserReadDto>
    {
        public UserController(UserManagerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
        }
        [HttpPost("{userId:int}/feature/{featureId:int}")]
        public async Task<ActionResult> SetUserFeature(int userId,int featureId, string featureValue)
        {
            User user = ctx.Set<User>().Find(userId) ?? throw new ArgumentException(nameof(userId));

            var result = await user.SetFeatureValue(featureId, featureValue,ctx);
            return Ok(new StandardResponse<KeyValuePair<string,string>>(true, "Value Set", result));
        }

        [HttpGet("{userId:int}/[action]")]
        public async Task<ActionResult> GetAllFeatures(int userId)
        {
            User user = ctx.Set<User>().Find(userId) ?? throw new ArgumentException(nameof(userId));

            var result = await user.GetAllFeatures(ctx);
            return Ok(new StandardResponse<List<KeyValuePair<int, string>>>(true, "Get All Feature Values", result));
        }
    }
}