using Infrastructure.BaseUserManager.Models;

namespace Infrastructure.BaseUserManager.IRepository
{
    public interface IUserRepositiry : IBaseRepository<User, UserCreateDto, UserReadDto, UserListDto>
    {
        Task<UserFeatureReadDto> SetFeatureValueAsync(Guid userId, Guid featureId, string featureValue);
        Task<string> GetFeatureValueAsync(Guid userId, Guid FeatureId);
        IQueryable<UserFeatureListDto> GetAllFeatures(Guid userId);
        Task<bool> RemoveFeatureValueAsync(Guid userId, Guid featureId);

    }
}