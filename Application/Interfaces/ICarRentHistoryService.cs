using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarRentHistoryService
    {
        Task<List<CarRentHistory>?> GetAllAsync();
        Task<CarRentHistory?> GetByIdAsync(int id);
        Task<CarRentHistory?> CreateAsync(CarRentHistory history);
        Task<CarRentHistory?> UpdateAsync(CarRentHistory history);
        Task<bool> DeleteAsync(int id);
    }

}
