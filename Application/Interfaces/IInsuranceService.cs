using Core.Entities;

namespace Application.Interfaces
{
    public interface IInsuranceService
    {
        Task<List<Insurance>?> GetAllAsync();
        Task<Insurance?> GetByIdAsync(int id);
        Task<Insurance?> CreateAsync(Insurance insurance);
        Task<Insurance?> UpdateAsync(Insurance insurance);
        Task<bool> DeleteAsync(int id);
    }

}
