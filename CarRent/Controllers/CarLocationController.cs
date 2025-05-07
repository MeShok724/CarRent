using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarLocationController : ControllerBase
    {
        private readonly ICarLocationService _service;

        public CarLocationController(ICarLocationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarLocation>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarLocation>> Get(int id)
        {
            var location = await _service.GetByIdAsync(id);
            if (location == null) return NotFound();
            return Ok(location);
        }

        [HttpPost]
        public async Task<ActionResult<CarLocation>> Create(CarLocation location)
        {
            var created = await _service.CreateAsync(location);
            if (created == null) return BadRequest("Failed to create car location");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarLocation>> Update(int id, CarLocation location)
        {
            if (id != location.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(location);
            if (updated == null) return NotFound("Location not found or update failed");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound("Location not found or deletion failed");
            return NoContent();
        }
    }

}
