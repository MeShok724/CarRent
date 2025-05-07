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

    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>?> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Branch)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Branch)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee?> CreateAsync(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Employee?> UpdateAsync(Employee employee)
        {
            try
            {
                var existing = await _context.Employees.FindAsync(employee.Id);
                if (existing == null) return null;

                existing.Firstname = employee.Firstname;
                existing.Lastname = employee.Lastname;
                existing.Email = employee.Email;
                existing.Phone = employee.Phone;
                existing.RoleId = employee.RoleId;
                existing.BranchId = employee.BranchId;

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
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null) return false;

                _context.Employees.Remove(employee);
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
