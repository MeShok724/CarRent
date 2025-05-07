using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserStatusController : ControllerBase
    {
        private readonly IUserStatusService _service;

        public UserStatusController(IUserStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserStatus>>> GetAll()
        {
            var statuses = await _service.GetAllAsync();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserStatus>> Get(int id)
        {
            var status = await _service.GetByIdAsync(id);
            if (status == null) return NotFound();
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<UserStatus>> Create(UserStatus userStatus)
        {
            var created = await _service.CreateAsync(userStatus);
            if (created == null) return BadRequest("Creation failed");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserStatus>> Update(int id, UserStatus userStatus)
        {
            if (id != userStatus.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(userStatus);
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
