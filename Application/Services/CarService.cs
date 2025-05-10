using System;
using System.Linq;
using System.Text;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Application.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _dbContext;

        public CarService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Cars
                    .Include(c => c.Category)
                    .Include(c => c.Status)
                    .Include(c => c.Branch)
                    .Include(c => c.CarImages)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Car>> GetCarsByCategoryAsync(int categoryId)
        {
            try
            {
                var cars = await _dbContext.Cars
                    .FromSqlInterpolated($"SELECT * FROM get_cars_by_category({categoryId})")
                    .Include(c => c.Category)
                    .Include(c => c.Status)
                    .Include(c => c.CarImages)
                    .ToListAsync();

                return cars;
            }
            catch (Exception)
            {
                return new List<Car>();
            }
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            try
            {
                return await _dbContext.Cars
                    .Include(c => c.Category)
                    .Include(c => c.Status)
                    .Include(c => c.Branch)
                    .Include(c => c.CarImages)
                    .OrderBy(c => c.Brand)
                    .ThenBy(c => c.Model)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Car>();
            }
        }

        public async Task<List<Car>> GetAvailableCarsAsync()
        {
            try
            {
                return await _dbContext.Cars
                    .FromSqlRaw("SELECT * FROM get_available_cars()")
                    .Include(c => c.Category)
                    .Include(c => c.Status)
                    .Include(c => c.CarImages)
                    .OrderBy(c => c.Brand)
                    .ThenBy(c => c.Model)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Car>();
            }
        }

        public async Task<Car?> CreateCarAsync(Car car)
        {
            try
            {
                // Проверка уникальности VIN и номерного знака
                if (!await CheckVinUniqueAsync(car.Vin))
                    return null;

                if (!await CheckLicensePlateUniqueAsync(car.LicensePlate))
                    return null;

                // Проверка существования связанных сущностей
                if (car.CategoryId.HasValue && !await _dbContext.CarCategories.AnyAsync(cc => cc.Id == car.CategoryId.Value))
                    return null;

                if (car.StatusId.HasValue && !await _dbContext.CarStatuses.AnyAsync(cs => cs.Id == car.StatusId.Value))
                    return null;

                if (car.BranchId.HasValue && !await _dbContext.Branches.AnyAsync(b => b.Id == car.BranchId.Value))
                    return null;

                car.AddedAt = DateTime.UtcNow;
                car.UpdatedAt = DateTime.UtcNow;

                await _dbContext.Cars.AddAsync(car);
                await _dbContext.SaveChangesAsync();

                return car;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Car?> UpdateCarAsync(int id, Car updatedCar)
        {
            try
            {
                var existingCar = await _dbContext.Cars.FindAsync(id);
                if (existingCar == null)
                    return null;

                // Проверка уникальности VIN (если изменился)
                if (existingCar.Vin != updatedCar.Vin && !await CheckVinUniqueAsync(updatedCar.Vin))
                    return null;

                // Проверка уникальности номерного знака (если изменился)
                if (existingCar.LicensePlate != updatedCar.LicensePlate &&
                    !await CheckLicensePlateUniqueAsync(updatedCar.LicensePlate))
                    return null;

                // Проверка существования связанных сущностей
                if (updatedCar.CategoryId.HasValue &&
                    !await _dbContext.CarCategories.AnyAsync(cc => cc.Id == updatedCar.CategoryId.Value))
                    return null;

                if (updatedCar.StatusId.HasValue &&
                    !await _dbContext.CarStatuses.AnyAsync(cs => cs.Id == updatedCar.StatusId.Value))
                    return null;

                if (updatedCar.BranchId.HasValue &&
                    !await _dbContext.Branches.AnyAsync(b => b.Id == updatedCar.BranchId.Value))
                    return null;

                // Обновление полей
                existingCar.Brand = updatedCar.Brand;
                existingCar.Model = updatedCar.Model;
                existingCar.Year = updatedCar.Year;
                existingCar.Vin = updatedCar.Vin;
                existingCar.LicensePlate = updatedCar.LicensePlate;
                existingCar.Color = updatedCar.Color;
                existingCar.Mileage = updatedCar.Mileage;
                existingCar.CategoryId = updatedCar.CategoryId;
                existingCar.StatusId = updatedCar.StatusId;
                existingCar.BranchId = updatedCar.BranchId;
                existingCar.RentalPricePerDay = updatedCar.RentalPricePerDay;
                existingCar.EngineVolume = updatedCar.EngineVolume;
                existingCar.Seats = updatedCar.Seats;
                existingCar.UpdatedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
                return existingCar;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            try
            {
                var car = await _dbContext.Cars.FindAsync(id);
                if (car == null)
                    return false;

                // Проверка, есть ли связанные заказы
                var hasOrders = await _dbContext.Orders.AnyAsync(o => o.CarId == id);
                if (hasOrders)
                    return false;

                _dbContext.Cars.Remove(car);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CheckVinUniqueAsync(string vin)
        {
            if (string.IsNullOrWhiteSpace(vin))
                return false;

            return !await _dbContext.Cars.AnyAsync(c => c.Vin == vin);
        }

        public async Task<bool> CheckLicensePlateUniqueAsync(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                return false;

            return !await _dbContext.Cars.AnyAsync(c => c.LicensePlate == licensePlate);
        }
    }
}
