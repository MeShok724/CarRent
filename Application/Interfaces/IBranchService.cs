using Core.Entities;

namespace Application.Interfaces
{
    public interface IBranchService
    {
        Task<Branch?> GetBranchByIdAsync(int id);
        Task<List<Branch>> GetAllBranchesAsync();
        Task<Branch?> CreateBranchAsync(Branch branch);
        Task<Branch?> UpdateBranchAsync(int id, Branch updatedBranch);
        Task<bool> DeleteBranchAsync(int id);
    }
}
