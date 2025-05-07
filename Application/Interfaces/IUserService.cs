using Core.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<User>?> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> CreateAsync(User user);
        Task<User?> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<User?> AuthenticateAsync(string username, string password);
    }

}
