using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICarImageService
    {
        Task<List<CarImage>> GetAllAsync();
        Task<CarImage?> GetByIdAsync(int id);
        Task<CarImage?> CreateAsync(CarImage image);
        Task<bool> DeleteAsync(int id);
    }

}
