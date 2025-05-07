using Core.Entities;

namespace Application.Interfaces
{
    public interface IBlacklistService
    {
        Task<Blacklist?> AddToBlacklistAsync(int userId, string reason, DateTime? expirationDate);
        Task<bool> RemoveFromBlacklistAsync(int blacklistRecordId);
        Task<List<Blacklist>> GetBlacklistRecordsAsync();
        Task<Blacklist?> GetBlacklistRecordAsync(int blacklistRecordId);
        Task<List<Blacklist>> GetUserBlacklistHistoryAsync(int userId);
        Task<bool> IsUserBannedAsync(int userId);
        Task<Blacklist?> UpdateBlacklistRecordAsync(int blacklistRecordId, string? reason, DateTime? expirationDate);
    }
}
