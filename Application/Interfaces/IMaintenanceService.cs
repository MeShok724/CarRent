using Core.Entities;

namespace Application.Interfaces
{
    public interface IMaintenanceService
    {
        Task<List<Maintenance>?> GetAllAsync();
        Task<Maintenance?> GetByIdAsync(int id);
        Task<Maintenance?> CreateAsync(Maintenance maintenance);
        Task<Maintenance?> UpdateAsync(Maintenance maintenance);
        Task<bool> DeleteAsync(int id);
    }

}
