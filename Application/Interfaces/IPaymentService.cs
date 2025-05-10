using Core.Entities;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<List<Payment>?> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment?> CreateAsync(Payment payment);
        Task<Payment?> UpdateAsync(Payment payment);
        Task<bool> DeleteAsync(int id);
        Task<decimal> GetOrderTotalPaymentsAsync(int orderId);
    }

}
