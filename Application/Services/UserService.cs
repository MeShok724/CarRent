using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Postgres;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<User>?> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Status).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Status)
                                       .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> CreateAsync(User user)
        {
            try
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User?> UpdateAsync(User user)
        {
            try
            {
                var existing = await _context.Users.FindAsync(user.Id);
                if (existing == null) return null;

                existing.Username = user.Username;
                existing.Email = user.Email;
                existing.PasswordHash = _passwordHasher.HashPassword(existing, user.PasswordHash);
                existing.StatusId = user.StatusId;

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
                var user = await _context.Users.FindAsync(id);
                if (user == null) return false;

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.Include(u => u.Status)
                                           .FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return user;
        }
    }

}
