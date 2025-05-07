using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarStatusController : ControllerBase
    {
        private readonly ICarStatusService _service;

        public CarStatusController(ICarStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarStatus>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarStatus>> Get(int id)
        {
            var status = await _service.GetByIdAsync(id);
            if (status == null) return NotFound();
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<CarStatus>> Create(CarStatus status)
        {
            var created = await _service.CreateAsync(status);
            if (created == null) return BadRequest("Creation failed");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarStatus>> Update(int id, CarStatus status)
        {
            if (id != status.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(status);
            if (updated == null) return NotFound("Not found or update failed");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound("Not found or deletion failed");
            return NoContent();
        }
    }

}
