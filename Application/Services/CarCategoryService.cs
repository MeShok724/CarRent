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

    public class CarCategoryService : ICarCategoryService
    {
        private readonly AppDbContext _context;

        public CarCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarCategory>?> GetAllAsync()
        {
            return await _context.CarCategories.ToListAsync();
        }

        public async Task<CarCategory?> GetByIdAsync(int id)
        {
            return await _context.CarCategories.FindAsync(id);
        }

        public async Task<CarCategory?> CreateAsync(CarCategory category)
        {
            try
            {
                _context.CarCategories.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CarCategory?> UpdateAsync(CarCategory category)
        {
            try
            {
                var existing = await _context.CarCategories.FindAsync(category.Id);
                if (existing == null) return null;

                existing.Name = category.Name;
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
                var category = await _context.CarCategories.FindAsync(id);
                if (category == null) return false;

                _context.CarCategories.Remove(category);
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
