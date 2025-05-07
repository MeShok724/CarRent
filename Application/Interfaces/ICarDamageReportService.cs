using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarDamageReportService
    {
        Task<List<CarDamageReport>?> GetAllAsync();
        Task<CarDamageReport?> GetByIdAsync(int id);
        Task<CarDamageReport?> CreateAsync(CarDamageReport report);
        Task<CarDamageReport?> UpdateAsync(CarDamageReport report);
        Task<bool> DeleteAsync(int id);
    }
}
