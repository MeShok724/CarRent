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

    public class EmployeeRoleService : IEmployeeRoleService
    {
        private readonly AppDbContext _context;

        public EmployeeRoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeRole>?> GetAllAsync()
        {
            return await _context.EmployeeRoles.ToListAsync();
        }

        public async Task<EmployeeRole?> GetByIdAsync(int id)
        {
            return await _context.EmployeeRoles.FindAsync(id);
        }

        public async Task<EmployeeRole?> CreateAsync(EmployeeRole role)
        {
            try
            {
                _context.EmployeeRoles.Add(role);
                await _context.SaveChangesAsync();
                return role;
            }
            catch
            {
                return null;
            }
        }

        public async Task<EmployeeRole?> UpdateAsync(EmployeeRole role)
        {
            try
            {
                var existing = await _context.EmployeeRoles.FindAsync(role.Id);
                if (existing == null) return null;

                existing.Name = role.Name;
                existing.Description = role.Description;

                await _context.SaveChangesAsync();
                return existing;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var role = await _context.EmployeeRoles.FindAsync(id);
                if (role == null) return false;

                _context.EmployeeRoles.Remove(role);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
