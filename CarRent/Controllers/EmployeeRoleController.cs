using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeRoleController : ControllerBase
    {
        private readonly IEmployeeRoleService _service;

        public EmployeeRoleController(IEmployeeRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeRole>>> GetAll()
        {
            var roles = await _service.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeRole>> Get(int id)
        {
            var role = await _service.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeRole>> Create(EmployeeRole role)
        {
            var created = await _service.CreateAsync(role);
            if (created == null) return BadRequest("Creation failed");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeRole>> Update(int id, EmployeeRole role)
        {
            if (id != role.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(role);
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
