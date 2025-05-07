using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarReviewService
    {
        Task<List<CarReview>?> GetAllAsync();
        Task<CarReview?> GetByIdAsync(int id);
        Task<CarReview?> CreateAsync(CarReview review);
        Task<CarReview?> UpdateAsync(CarReview review);
        Task<bool> DeleteAsync(int id);
    }

}
