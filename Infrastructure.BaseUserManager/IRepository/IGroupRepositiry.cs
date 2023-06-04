using Infrastructure.BaseDataProvider.Repository;
using Infrastructure.BaseUserManager.Models;
using System.Linq;

namespace Infrastructure.BaseUserManager.IRepository
{
    public interface IGroupRepositiry : IBaseRepository<Group, GroupCreateDto, GroupReadDto, GroupListDto>
    {
        Task<Guid> AddUserToGroupAsync(Guid groupId, Guid UserId, string Name);
        Task RemoveUserFromGroupAsync(Guid groupId, Guid UserId);
    }
}