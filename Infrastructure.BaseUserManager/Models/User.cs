using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Infrastructure.BaseUserManager.Models
{
    public class User : BaseEntity<User, UserDto, UserReadDto>
    {

        [ForeignKey(nameof(UserType))]
        public int UserTypeId { get; set; }
        [JsonIgnore]
        public virtual UserType UserType { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserFeature>? UserFeatures { get; set; }

        public virtual ICollection<UserGroup>? UserGroup { get; set; }

    }

    public class UserDto : BaseDto<User, UserDto, UserReadDto>
    {
        public int ContactTypeId { get; set; }
    }
    public class UserReadDto : BaseReadDto<User, UserDto, UserReadDto>
    {
        public int ContactTypeId { get; set; }
        public string ContactTypeTitle { get; set; } = string.Empty;
    }

    public static class UserRepositoryHelper
    {
        public static async Task<KeyValuePair<string,string>> SetFeatureValue(this User user, int featureId, string featureValue,ApplicationDbContext context)
        {
            var userFeature = await context.Set<UserFeature>().FirstOrDefaultAsync(c=>c.UserId == user.Id && c.FeatureId == featureId);
            if (userFeature == null)
            {
                userFeature = new UserFeature
                {
                    FeatureId = featureId,
                    UserId = user.Id,
                    Value = featureValue
                };
                context.Set<UserFeature>().Add(userFeature);
            }
            else
            {
                userFeature.Value = featureValue;
                context.Entry(userFeature).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            await context.SaveChangesAsync();
            return new KeyValuePair<string, string>(userFeature.Name,userFeature.Value);
        }

        public static async Task<string> GetFeatureValue(this User user, int FeatureId,ApplicationDbContext context)
        {
            UserFeature userFeature = await context.Set<UserFeature>().FirstOrDefaultAsync(c=>c.UserId == user.Id && c.FeatureId ==  FeatureId)
                ?? throw new ArgumentException(nameof(FeatureId));

            return userFeature.Value;
        }

        public static async Task<List<KeyValuePair<int,string>>> GetAllFeatures(this User user, ApplicationDbContext context)
        {
            var userFeatures = await context.Set<UserFeature>().Where(c => c.UserId == user.Id).Select(c=> new KeyValuePair<int, string>(c.FeatureId,c.Value)).ToListAsync();

            return userFeatures;
        }
    }
}
