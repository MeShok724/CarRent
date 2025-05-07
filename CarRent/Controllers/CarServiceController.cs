using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarCategoryController : ControllerBase
    {
        private readonly ICarCategoryService _service;

        public CarCategoryController(ICarCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarCategory>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarCategory>> Get(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CarCategory>> Create(CarCategory category)
        {
            var created = await _service.CreateAsync(category);
            if (created == null) return BadRequest("Failed to create category");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarCategory>> Update(int id, CarCategory category)
        {
            if (id != category.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(category);
            if (updated == null) return NotFound("Category not found or update failed");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound("Category not found or deletion failed");
            return NoContent();
        }
    }
}
