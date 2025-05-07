using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAll()
        {
            var employees = await _service.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            var created = await _service.CreateAsync(employee);
            if (created == null) return BadRequest("Creation failed");
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Update(int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(employee);
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
