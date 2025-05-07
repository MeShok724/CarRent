using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarRentHistoryController : ControllerBase
    {
        private readonly ICarRentHistoryService _service;

        public CarRentHistoryController(ICarRentHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarRentHistory>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarRentHistory>> Get(int id)
        {
            var history = await _service.GetByIdAsync(id);
            if (history == null) return NotFound();
            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<CarRentHistory>> Create(CarRentHistory history)
        {
            var created = await _service.CreateAsync(history);
            if (created == null) return BadRequest("Failed to create rent history record");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarRentHistory>> Update(int id, CarRentHistory history)
        {
            if (id != history.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(history);
            if (updated == null) return NotFound("Record not found or update failed");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound("Record not found or deletion failed");
            return NoContent();
        }
    }

}
