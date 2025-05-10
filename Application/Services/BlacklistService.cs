using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{

    public class BlacklistService : IBlacklistService
    {
        private readonly AppDbContext _dbContext;

        public BlacklistService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Blacklist?> AddToBlacklistAsync(int userId, string reason, DateTime? expirationDate)
        {
            try
            {
                var userExists = await _dbContext.Users.AnyAsync(u => u.Id == userId);
                if (!userExists)
                    return null;

                var days = expirationDate.HasValue
                    ? (int)Math.Ceiling((expirationDate.Value - DateTime.UtcNow).TotalDays)
                    : 30;

                await _dbContext.Database.ExecuteSqlInterpolatedAsync(
                    $"CALL add_to_blacklist({userId}, {reason}, {days})");

                var blackList = await _dbContext.Blacklists.FirstAsync(b => b.UserId == userId);
                return blackList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> RemoveFromBlacklistAsync(int blacklistRecordId)
        {
            try
            {
                var record = await _dbContext.Blacklists.FindAsync(blacklistRecordId);
                if (record == null)
                    return false;

                _dbContext.Blacklists.Remove(record);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<Blacklist>> GetBlacklistRecordsAsync()
        {
            try
            {
                return await _dbContext.Blacklists.ToListAsync();
            }
            catch (Exception)
            {
                return new List<Blacklist>();
            }
        }

        public async Task<Blacklist?> GetBlacklistRecordAsync(int blacklistRecordId)
        {
            try
            {
                return await _dbContext.Blacklists
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.Id == blacklistRecordId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Blacklist>> GetUserBlacklistHistoryAsync(int userId)
        {
            try
            {
                return await _dbContext.Blacklists
                    .Where(b => b.UserId == userId)
                    .OrderByDescending(b => b.BunnedAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Blacklist>();
            }
        }

        public async Task<bool> IsUserBannedAsync(int userId)
        {
            try
            {
                var currentDate = DateTime.UtcNow;
                return await _dbContext.Blacklists
                    .AnyAsync(b => b.UserId == userId &&
                                 (b.ExpirationDate == null || b.ExpirationDate > currentDate));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Blacklist?> UpdateBlacklistRecordAsync(int blacklistRecordId, string? reason, DateTime? expirationDate)
        {
            try
            {
                var record = await _dbContext.Blacklists.FindAsync(blacklistRecordId);
                if (record == null)
                    return null;

                if (reason != null)
                    record.Reason = reason;

                if (expirationDate != null)
                    record.ExpirationDate = expirationDate;

                await _dbContext.SaveChangesAsync();
                return record;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}
