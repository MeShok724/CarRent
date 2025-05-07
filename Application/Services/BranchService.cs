using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{

    public class BranchService : IBranchService
    {
        private readonly AppDbContext _dbContext;

        public BranchService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Branch?> GetBranchByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Branches
                    .Include(b => b.Manager)
                    .FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Branch>> GetAllBranchesAsync()
        {
            try
            {
                return await _dbContext.Branches
                    .Include(b => b.Manager)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Branch>();
            }
        }

        public async Task<Branch?> CreateBranchAsync(Branch branch)
        {
            try
            {
                // Проверяем существует ли менеджер
                if (branch.ManagerId.HasValue)
                {
                    var managerExists = await _dbContext.Users.AnyAsync(u => u.Id == branch.ManagerId.Value);
                    if (!managerExists)
                        return null;
                }

                await _dbContext.Branches.AddAsync(branch);
                await _dbContext.SaveChangesAsync();
                return branch;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Branch?> UpdateBranchAsync(int id, Branch updatedBranch)
        {
            try
            {
                var existingBranch = await _dbContext.Branches.FindAsync(id);
                if (existingBranch == null)
                    return null;

                // Проверяем нового менеджера
                if (updatedBranch.ManagerId.HasValue)
                {
                    var managerExists = await _dbContext.Users.AnyAsync(u => u.Id == updatedBranch.ManagerId.Value);
                    if (!managerExists)
                        return null;
                }

                // Обновляем поля
                existingBranch.Name = updatedBranch.Name;
                existingBranch.City = updatedBranch.City;
                existingBranch.Address = updatedBranch.Address;
                existingBranch.PostalCode = updatedBranch.PostalCode;
                existingBranch.Phone = updatedBranch.Phone;
                existingBranch.ManagerId = updatedBranch.ManagerId;

                await _dbContext.SaveChangesAsync();
                return existingBranch;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteBranchAsync(int id)
        {
            try
            {
                var branch = await _dbContext.Branches.FindAsync(id);
                if (branch == null)
                    return false;

                _dbContext.Branches.Remove(branch);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
