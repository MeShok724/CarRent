using Core.Entities;

namespace Application.Interfaces
{
    public interface IDiscountService
    {
        Task<List<Discount>?> GetAllAsync();
        Task<Discount?> GetByIdAsync(int id);
        Task<Discount?> CreateAsync(Discount discount);
        Task<Discount?> UpdateAsync(Discount discount);
        Task<bool> DeleteAsync(int id);
    }

}
