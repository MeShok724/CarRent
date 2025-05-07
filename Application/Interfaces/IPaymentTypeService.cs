using Core.Entities;

namespace Application.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<List<PaymentType>?> GetAllAsync();
        Task<PaymentType?> GetByIdAsync(int id);
        Task<PaymentType?> CreateAsync(PaymentType paymentType);
        Task<PaymentType?> UpdateAsync(PaymentType paymentType);
        Task<bool> DeleteAsync(int id);
    }

}
