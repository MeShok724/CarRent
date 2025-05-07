using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarDamageReportController : ControllerBase
    {
        private readonly ICarDamageReportService _service;

        public CarDamageReportController(ICarDamageReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDamageReport>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDamageReport>> Get(int id)
        {
            var report = await _service.GetByIdAsync(id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public async Task<ActionResult<CarDamageReport>> Create(CarDamageReport report)
        {
            var created = await _service.CreateAsync(report);
            if (created == null) return BadRequest("Failed to create damage report");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarDamageReport>> Update(int id, CarDamageReport report)
        {
            if (id != report.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(report);
            if (updated == null) return NotFound("Report not found or update failed");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound("Report not found or deletion failed");
            return NoContent();
        }
    }

}
