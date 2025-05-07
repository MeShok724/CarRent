using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarDocumentController : ControllerBase
    {
        private readonly ICarDocumentService _service;

        public CarDocumentController(ICarDocumentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDocument>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDocument>> Get(int id)
        {
            var document = await _service.GetByIdAsync(id);
            if (document == null) return NotFound();
            return Ok(document);
        }

        [HttpPost]
        public async Task<ActionResult<CarDocument>> Create(CarDocument document)
        {
            var created = await _service.CreateAsync(document);
            if (created == null) return BadRequest("Failed to create car document");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarDocument>> Update(int id, CarDocument document)
        {
            if (id != document.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(document);
            if (updated == null) return NotFound("Document not found or update failed");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound("Document not found or deletion failed");
            return NoContent();
        }
    }

}
