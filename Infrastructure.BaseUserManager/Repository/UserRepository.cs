using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.BaseDataProvider.Repository;
using Infrastructure.BaseDomain;
using Infrastructure.BaseUserManager.IRepository;
using Infrastructure.BaseUserManager.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BaseUserManager.Repository
{
    public class UserRepository : BaseRepository<User, UserCreateDto, UserReadDto, UserListDto>, IUserRepositiry
    {
        public UserRepository(ApplicationDbContext context,IMapper mapper):base(context,mapper)
        {
            
        }

        public async Task<UserFeatureReadDto> SetFeatureValueAsync(Guid userId, Guid featureId, string featureValue)
        {
            var userFeature = await context.Set<UserFeature>().FirstOrDefaultAsync(c => c.UserId == userId && c.FeatureId == featureId);

            if (userFeature == null)
            {
                userFeature = new UserFeature
                {
                    FeatureId = featureId,
                    UserId = userId,
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
            return userFeature.GetReadDto(mapper);
        }

        public async Task<string> GetFeatureValueAsync(Guid userId, Guid FeatureId)
        {
            UserFeature userFeature = await context.Set<UserFeature>().FirstOrDefaultAsync(c => c.UserId == userId && c.FeatureId == FeatureId)
                ?? throw new ArgumentException(nameof(FeatureId));

            return userFeature.Value;
        }

        public IQueryable<UserFeatureListDto> GetAllFeatures(Guid userId)
        {
            return context.Set<UserFeature>().Where(c => c.UserId == userId).ProjectTo<UserFeatureListDto>(mapper.ConfigurationProvider);
        }

        public async Task<bool> RemoveFeatureValueAsync(Guid userId, Guid featureId)
        {
            UserFeature userFeature = await context.Set<UserFeature>().FirstOrDefaultAsync(c => c.UserId == userId && c.FeatureId == featureId)
                ?? throw new ArgumentException(nameof(featureId));

            userFeature.DeleteEntity();

            context.Entry(userFeature).State = EntityState.Modified;

            return ((await context.SaveChangesAsync()) > 0);

        }
    }
}
