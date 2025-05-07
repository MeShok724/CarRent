using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _service;

        public MaintenanceController(IMaintenanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Maintenance>>> GetAll()
        {
            var maintenances = await _service.GetAllAsync();
            return Ok(maintenances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Maintenance>> Get(int id)
        {
            var maintenance = await _service.GetByIdAsync(id);
            if (maintenance == null) return NotFound();
            return Ok(maintenance);
        }

        [HttpPost]
        public async Task<ActionResult<Maintenance>> Create(Maintenance maintenance)
        {
            var created = await _service.CreateAsync(maintenance);
            if (created == null) return BadRequest("Creation failed");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Maintenance>> Update(int id, Maintenance maintenance)
        {
            if (id != maintenance.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(maintenance);
            if (updated == null) return NotFound("Update failed");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound("Deletion failed");
            return NoContent();
        }
    }

}
