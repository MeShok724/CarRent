using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly ILogger<BranchesController> _logger;

        public BranchesController(IBranchService branchService, ILogger<BranchesController> logger)
        {
            _branchService = branchService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(int id)
        {
            try
            {
                var branch = await _branchService.GetBranchByIdAsync(id);
                return branch != null ? Ok(branch) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting branch with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            try
            {
                var branches = await _branchService.GetAllBranchesAsync();
                return Ok(branches);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all branches");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch([FromBody] Branch branch)
        {
            try
            {
                var createdBranch = await _branchService.CreateBranchAsync(branch);
                return createdBranch != null
                    ? CreatedAtAction(nameof(GetBranch), new { id = createdBranch.Id }, createdBranch)
                    : BadRequest("Failed to create branch or manager doesn't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating branch");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, [FromBody] Branch branch)
        {
            try
            {
                var updatedBranch = await _branchService.UpdateBranchAsync(id, branch);
                return updatedBranch != null
                    ? Ok(updatedBranch)
                    : NotFound("Branch not found or manager doesn't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating branch with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            try
            {
                var success = await _branchService.DeleteBranchAsync(id);
                return success ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting branch with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
