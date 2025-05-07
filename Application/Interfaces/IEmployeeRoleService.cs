using Core.Entities;

namespace Application.Interfaces
{
    public interface IEmployeeRoleService
    {
        Task<List<EmployeeRole>?> GetAllAsync();
        Task<EmployeeRole?> GetByIdAsync(int id);
        Task<EmployeeRole?> CreateAsync(EmployeeRole role);
        Task<EmployeeRole?> UpdateAsync(EmployeeRole role);
        Task<bool> DeleteAsync(int id);
    }

}
