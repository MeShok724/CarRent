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

    public class CarLocationService : ICarLocationService
    {
        private readonly AppDbContext _context;

        public CarLocationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarLocation>?> GetAllAsync()
        {
            return await _context.CarLocations
                .Include(l => l.Car)
                .ToListAsync();
        }

        public async Task<CarLocation?> GetByIdAsync(int id)
        {
            return await _context.CarLocations
                .Include(l => l.Car)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<CarLocation?> CreateAsync(CarLocation location)
        {
            try
            {
                _context.CarLocations.Add(location);
                await _context.SaveChangesAsync();
                return location;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CarLocation?> UpdateAsync(CarLocation location)
        {
            try
            {
                var existing = await _context.CarLocations.FindAsync(location.Id);
                if (existing == null) return null;

                existing.CarId = location.CarId;
                existing.Latitude = location.Latitude;
                existing.Longitude = location.Longitude;
                existing.UpdatedAt = location.UpdatedAt;

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
                var location = await _context.CarLocations.FindAsync(id);
                if (location == null) return false;

                _context.CarLocations.Remove(location);
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
