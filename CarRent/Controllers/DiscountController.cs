using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _service;

        public DiscountController(IDiscountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Discount>>> GetAll()
        {
            var discounts = await _service.GetAllAsync();
            return Ok(discounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> Get(int id)
        {
            var discount = await _service.GetByIdAsync(id);
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [HttpPost]
        public async Task<ActionResult<Discount>> Create(Discount discount)
        {
            var created = await _service.CreateAsync(discount);
            if (created == null) return BadRequest("Creation failed");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Discount>> Update(int id, Discount discount)
        {
            if (id != discount.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(discount);
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
