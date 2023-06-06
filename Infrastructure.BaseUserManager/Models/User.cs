using AutoMapper.Features;
using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Infrastructure.BaseUserManager.Models
{
    public class User : BaseEntity<User, UserCreateDto, UserReadDto,UserListDto>
    {

        [ForeignKey(nameof(UserType))]
        public Guid UserTypeId { get; set; }
        [JsonIgnore]
        public virtual UserType UserType { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserFeature>? UserFeatures { get; set; }

        public virtual ICollection<UserGroup>? UserGroup { get; set; }

    }

    class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
        }
    }

    public class UserCreateDto : BaseCreateDto<User, UserCreateDto, UserReadDto, UserListDto>
    {
        public Guid UserTypeId { get; set; }
    }
    public class UserReadDto : BaseReadDto<User, UserCreateDto, UserReadDto, UserListDto>
    {
        public Guid UserTypeId { get; set; }
        public string UserTypeTitle { get; set; } = string.Empty;
    }

    public class UserListDto : BaseListDto<User, UserCreateDto, UserReadDto, UserListDto>
    {
        public Guid UserTypeId { get; set; }
    }

    public static class UserRepositoryHelper
    {
        public static async Task<KeyValuePair<string,string>> SetFeatureValue(this User user, Guid featureId, string featureValue,ApplicationDbContext context)
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

        public static async Task<string> GetFeatureValue(this User user, Guid FeatureId,ApplicationDbContext context)
        {
            UserFeature userFeature = await context.Set<UserFeature>().FirstOrDefaultAsync(c=>c.UserId == user.Id && c.FeatureId ==  FeatureId)
                ?? throw new ArgumentException(nameof(FeatureId));

            return userFeature.Value;
        }

        public static async Task<List<KeyValuePair<Guid,string>>> GetAllFeatures(this User user, ApplicationDbContext context)
        {
            var userFeatures = await context.Set<UserFeature>().Where(c => c.UserId == user.Id).Select(c=> new KeyValuePair<Guid, string>(c.FeatureId,c.Value)).ToListAsync();

            return userFeatures;
        }
    
        public static async Task<bool> RemoveFeatureValue(this User user, Guid featureId,ApplicationDbContext context)
        {
            UserFeature userFeature = await context.Set<UserFeature>().FirstOrDefaultAsync(c => c.UserId == user.Id && c.FeatureId == featureId)
                ?? throw new ArgumentException(nameof(featureId));

            userFeature.DeleteEntity();

            context.Entry(userFeature).State = EntityState.Modified;

            return ((await context.SaveChangesAsync()) > 0);

        }
    }
}
