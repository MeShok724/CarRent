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

    public class UserStatusService : IUserStatusService
    {
        private readonly AppDbContext _context;

        public UserStatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserStatus>?> GetAllAsync()
        {
            return await _context.UserStatuses.ToListAsync();
        }

        public async Task<UserStatus?> GetByIdAsync(int id)
        {
            return await _context.UserStatuses.FirstOrDefaultAsync(us => us.Id == id);
        }

        public async Task<UserStatus?> CreateAsync(UserStatus userStatus)
        {
            try
            {
                _context.UserStatuses.Add(userStatus);
                await _context.SaveChangesAsync();
                return userStatus;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserStatus?> UpdateAsync(UserStatus userStatus)
        {
            try
            {
                var existing = await _context.UserStatuses.FindAsync(userStatus.Id);
                if (existing == null) return null;

                existing.Name = userStatus.Name;

                await _context.SaveChangesAsync();
                return existing;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var userStatus = await _context.UserStatuses.FindAsync(id);
                if (userStatus == null) return false;

                _context.UserStatuses.Remove(userStatus);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
