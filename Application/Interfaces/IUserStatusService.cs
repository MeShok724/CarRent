using Core.Entities;

namespace Application.Interfaces
{
    public interface IUserStatusService
    {
        Task<List<UserStatus>?> GetAllAsync();
        Task<UserStatus?> GetByIdAsync(int id);
        Task<UserStatus?> CreateAsync(UserStatus userStatus);
        Task<UserStatus?> UpdateAsync(UserStatus userStatus);
        Task<bool> DeleteAsync(int id);
    }

}
