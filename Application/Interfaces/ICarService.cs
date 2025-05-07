namespace Application.Interfaces
{
    using Core.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICarService
    {
        Task<Car?> GetCarByIdAsync(int id);
        Task<List<Car>> GetAllCarsAsync();
        Task<List<Car>> GetAvailableCarsAsync();
        Task<Car?> CreateCarAsync(Car car);
        Task<Car?> UpdateCarAsync(int id, Car updatedCar);
        Task<bool> DeleteCarAsync(int id);
        Task<bool> CheckVinUniqueAsync(string vin);
        Task<bool> CheckLicensePlateUniqueAsync(string licensePlate);
    }
}
