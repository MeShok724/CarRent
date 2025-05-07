using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarLocationService
    {
        Task<List<CarLocation>?> GetAllAsync();
        Task<CarLocation?> GetByIdAsync(int id);
        Task<CarLocation?> CreateAsync(CarLocation location);
        Task<CarLocation?> UpdateAsync(CarLocation location);
        Task<bool> DeleteAsync(int id);
    }

}
