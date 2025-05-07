using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/blacklist")]
    public class BlacklistController : ControllerBase
    {
        private readonly IBlacklistService _blacklistService;
        private readonly ILogger<BlacklistController> _logger;

        public BlacklistController(IBlacklistService blacklistService, ILogger<BlacklistController> logger)
        {
            _blacklistService = blacklistService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddToBlacklist([FromBody] AddToBlacklistRequest request)
        {
            try
            {
                var result = await _blacklistService.AddToBlacklistAsync(request.UserId, request.Reason, request.ExpirationDate);

                if (result == null)
                    return BadRequest("Failed to add to blacklist or user doesn't exist");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding to blacklist");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromBlacklist(int id)
        {
            try
            {
                var success = await _blacklistService.RemoveFromBlacklistAsync(id);
                return success ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing from blacklist");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetBlacklistRecords()
        {
            try
            {
                var record = await _blacklistService.GetBlacklistRecordsAsync();
                return record != null ? Ok(record) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting blacklist record");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlacklistRecord(int id)
        {
            try
            {
                var record = await _blacklistService.GetBlacklistRecordAsync(id);
                return record != null ? Ok(record) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting blacklist record");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBlacklistHistory(int userId)
        {
            try
            {
                var history = await _blacklistService.GetUserBlacklistHistoryAsync(userId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user blacklist history");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("is-banned/{userId}")]
        public async Task<IActionResult> IsUserBanned(int userId)
        {
            try
            {
                var isBanned = await _blacklistService.IsUserBannedAsync(userId);
                return Ok(new { IsBanned = isBanned });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if user is banned");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlacklistRecord(int id, [FromBody] UpdateBlacklistRequest request)
        {
            try
            {
                var updatedRecord = await _blacklistService.UpdateBlacklistRecordAsync(
                    id, request.Reason, request.ExpirationDate);

                return updatedRecord != null ? Ok(updatedRecord) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating blacklist record");
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class AddToBlacklistRequest
    {
        public int UserId { get; set; }
        public string Reason { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }

    public class UpdateBlacklistRequest
    {
        public string? Reason { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
