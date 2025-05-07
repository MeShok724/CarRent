using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarStatusService
    {
        Task<List<CarStatus>?> GetAllAsync();
        Task<CarStatus?> GetByIdAsync(int id);
        Task<CarStatus?> CreateAsync(CarStatus status);
        Task<CarStatus?> UpdateAsync(CarStatus status);
        Task<bool> DeleteAsync(int id);
    }

}
