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
    public class CarImageService : ICarImageService
    {
        private readonly AppDbContext _context;

        public CarImageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarImage>> GetAllAsync()
        {
            return await _context.CarImages.Include(ci => ci.Car).ToListAsync();
        }

        public async Task<CarImage?> GetByIdAsync(int id)
        {
            return await _context.CarImages.Include(ci => ci.Car).FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<CarImage?> CreateAsync(CarImage image)
        {
            _context.CarImages.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var image = await _context.CarImages.FindAsync(id);
            if (image == null)
                return false;

            _context.CarImages.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
