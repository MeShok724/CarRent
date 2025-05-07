using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarReviewController : ControllerBase
    {
        private readonly ICarReviewService _service;

        public CarReviewController(ICarReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarReview>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarReview>> Get(int id)
        {
            var review = await _service.GetByIdAsync(id);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpPost]
        public async Task<ActionResult<CarReview>> Create(CarReview review)
        {
            var created = await _service.CreateAsync(review);
            if (created == null) return BadRequest("Failed to create review");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarReview>> Update(int id, CarReview review)
        {
            if (id != review.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(review);
            if (updated == null) return NotFound("Review not found or update failed");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound("Review not found or deletion failed");
            return NoContent();
        }
    }

}
