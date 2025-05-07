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

    public class CarStatusService : ICarStatusService
    {
        private readonly AppDbContext _context;

        public CarStatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarStatus>?> GetAllAsync()
        {
            return await _context.CarStatuses.ToListAsync();
        }

        public async Task<CarStatus?> GetByIdAsync(int id)
        {
            return await _context.CarStatuses.FindAsync(id);
        }

        public async Task<CarStatus?> CreateAsync(CarStatus status)
        {
            try
            {
                _context.CarStatuses.Add(status);
                await _context.SaveChangesAsync();
                return status;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CarStatus?> UpdateAsync(CarStatus status)
        {
            try
            {
                var existing = await _context.CarStatuses.FindAsync(status.Id);
                if (existing == null) return null;

                existing.Name = status.Name;
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
                var status = await _context.CarStatuses.FindAsync(id);
                if (status == null) return false;

                _context.CarStatuses.Remove(status);
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
