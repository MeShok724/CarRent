using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _service;

        public PaymentTypeController(IPaymentTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentType>>> GetAll()
        {
            var paymentTypes = await _service.GetAllAsync();
            return Ok(paymentTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentType>> Get(int id)
        {
            var paymentType = await _service.GetByIdAsync(id);
            if (paymentType == null) return NotFound();
            return Ok(paymentType);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentType>> Create(PaymentType paymentType)
        {
            var created = await _service.CreateAsync(paymentType);
            if (created == null) return BadRequest("Creation failed");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentType>> Update(int id, PaymentType paymentType)
        {
            if (id != paymentType.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(paymentType);
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
