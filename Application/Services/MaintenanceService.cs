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

    public class MaintenanceService : IMaintenanceService
    {
        private readonly AppDbContext _context;

        public MaintenanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Maintenance>?> GetAllAsync()
        {
            return await _context.Maintenance.ToListAsync();
        }

        public async Task<Maintenance?> GetByIdAsync(int id)
        {
            return await _context.Maintenance.FindAsync(id);
        }

        public async Task<Maintenance?> CreateAsync(Maintenance maintenance)
        {
            try
            {
                _context.Maintenance.Add(maintenance);
                await _context.SaveChangesAsync();
                return maintenance;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Maintenance?> UpdateAsync(Maintenance maintenance)
        {
            try
            {
                var existing = await _context.Maintenance.FindAsync(maintenance.Id);
                if (existing == null) return null;

                existing.Type = maintenance.Type;
                existing.Description = maintenance.Description;
                existing.Cost = maintenance.Cost;
                existing.Date = maintenance.Date;

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
                var maintenance = await _context.Maintenance.FindAsync(id);
                if (maintenance == null) return false;

                _context.Maintenance.Remove(maintenance);
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
