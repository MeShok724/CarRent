using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarService carService, ILogger<CarsController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            try
            {
                var car = await _carService.GetCarByIdAsync(id);
                return car != null ? Ok(car) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting car with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                var cars = await _carService.GetAllCarsAsync();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all cars");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableCars()
        {
            try
            {
                var availableCars = await _carService.GetAvailableCarsAsync();
                return Ok(availableCars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting available cars");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] Car car)
        {
            try
            {
                var createdCar = await _carService.CreateCarAsync(car);

                if (createdCar == null)
                    return BadRequest("Failed to create car. Possible reasons: " +
                        "VIN or License plate already exists, invalid category/status/branch reference");

                return CreatedAtAction(nameof(GetCar), new { id = createdCar.Id }, createdCar);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating car");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Car car)
        {
            try
            {
                if (id != car.Id)
                    return BadRequest("ID mismatch");

                var updatedCar = await _carService.UpdateCarAsync(id, car);

                if (updatedCar == null)
                    return BadRequest("Failed to update car. Possible reasons: " +
                        "Car not found, VIN or License plate already exists, invalid category/status/branch reference");

                return Ok(updatedCar);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating car with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                var success = await _carService.DeleteCarAsync(id);
                return success ? NoContent() : BadRequest("Car not found or has related orders");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting car with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("check-vin/{vin}")]
        public async Task<IActionResult> CheckVinUnique(string vin)
        {
            try
            {
                var isUnique = await _carService.CheckVinUniqueAsync(vin);
                return Ok(new { IsUnique = isUnique });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking VIN uniqueness: {vin}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("check-license/{licensePlate}")]
        public async Task<IActionResult> CheckLicensePlateUnique(string licensePlate)
        {
            try
            {
                var isUnique = await _carService.CheckLicensePlateUniqueAsync(licensePlate);
                return Ok(new { IsUnique = isUnique });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking license plate uniqueness: {licensePlate}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
