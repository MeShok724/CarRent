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

    public class DiscountService : IDiscountService
    {
        private readonly AppDbContext _context;

        public DiscountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Discount>?> GetAllAsync()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<Discount?> GetByIdAsync(int id)
        {
            return await _context.Discounts.FindAsync(id);
        }

        public async Task<Discount?> CreateAsync(Discount discount)
        {
            try
            {
                _context.Discounts.Add(discount);
                await _context.SaveChangesAsync();
                return discount;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Discount?> UpdateAsync(Discount discount)
        {
            try
            {
                var existing = await _context.Discounts.FindAsync(discount.Id);
                if (existing == null) return null;

                existing.Name = discount.Name;
                existing.IsActive = discount.IsActive;

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
                var discount = await _context.Discounts.FindAsync(id);
                if (discount == null) return false;

                _context.Discounts.Remove(discount);
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
