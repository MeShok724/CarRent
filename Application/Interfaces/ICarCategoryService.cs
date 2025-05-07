using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarCategoryService
    {
        Task<List<CarCategory>?> GetAllAsync();
        Task<CarCategory?> GetByIdAsync(int id);
        Task<CarCategory?> CreateAsync(CarCategory category);
        Task<CarCategory?> UpdateAsync(CarCategory category);
        Task<bool> DeleteAsync(int id);
    }
}
