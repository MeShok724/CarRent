using Core.Entities;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>?> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> CreateAsync(Order order);
        Task<Order?> UpdateAsync(Order order);
        Task<bool> DeleteAsync(int id);
    }

}
