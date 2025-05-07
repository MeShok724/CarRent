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

    public class CarRentHistoryService : ICarRentHistoryService
    {
        private readonly AppDbContext _context;

        public CarRentHistoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarRentHistory>?> GetAllAsync()
        {
            return await _context.CarRentHistory
                .Include(h => h.Car)
                .Include(h => h.Customer)
                .Include(h => h.Order)
                .ToListAsync();
        }

        public async Task<CarRentHistory?> GetByIdAsync(int id)
        {
            return await _context.CarRentHistory
                .Include(h => h.Car)
                .Include(h => h.Customer)
                .Include(h => h.Order)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<CarRentHistory?> CreateAsync(CarRentHistory history)
        {
            try
            {
                _context.CarRentHistory.Add(history);
                await _context.SaveChangesAsync();
                return history;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CarRentHistory?> UpdateAsync(CarRentHistory history)
        {
            try
            {
                var existing = await _context.CarRentHistory.FindAsync(history.Id);
                if (existing == null) return null;

                existing.CarId = history.CarId;
                existing.CustomerId = history.CustomerId;
                existing.OrderId = history.OrderId;
                existing.StartDate = history.StartDate;
                existing.EndDate = history.EndDate;

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
                var history = await _context.CarRentHistory.FindAsync(id);
                if (history == null) return false;

                _context.CarRentHistory.Remove(history);
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
