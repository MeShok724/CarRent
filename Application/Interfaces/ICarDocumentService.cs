using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarDocumentService
    {
        Task<List<CarDocument>?> GetAllAsync();
        Task<CarDocument?> GetByIdAsync(int id);
        Task<CarDocument?> CreateAsync(CarDocument document);
        Task<CarDocument?> UpdateAsync(CarDocument document);
        Task<bool> DeleteAsync(int id);
    }

}
