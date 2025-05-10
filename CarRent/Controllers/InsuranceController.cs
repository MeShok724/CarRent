using Application.Interfaces;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _service;

        public InsuranceController(IInsuranceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Insurance>>> GetAll()
        {
            var insurances = await _service.GetAllAsync();
            return Ok(insurances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Insurance>> Get(int id)
        {
            var insurance = await _service.GetByIdAsync(id);
            if (insurance == null) return NotFound();
            return Ok(insurance);
        }

        [HttpPost]
        public async Task<ActionResult<Insurance>> Create(Insurance insurance)
        {
            var created = await _service.CreateAsync(insurance);
            if (created == null) return BadRequest("Creation failed");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Insurance>> Update(int id, Insurance insurance)
        {
            if (id != insurance.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(insurance);
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

        [HttpPost("extend/{carId}")]
        public async Task<IActionResult> ExtendCarInsurance(int carId, [FromQuery] int monthsToAdd)
        {
            try
            {
                // Вызов метода сервиса для продления страховки
                await _service.ExtendCarInsuranceAsync(carId, monthsToAdd);
                return Ok(new { Message = "Car insurance extended successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }

}
