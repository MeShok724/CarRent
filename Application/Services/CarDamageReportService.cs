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

    public class CarDamageReportService : ICarDamageReportService
    {
        private readonly AppDbContext _context;

        public CarDamageReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarDamageReport>?> GetAllAsync()
        {
            return await _context.CarDamageReports
                .Include(r => r.Car)
                .Include(r => r.Employee)
                .ToListAsync();
        }

        public async Task<CarDamageReport?> GetByIdAsync(int id)
        {
            return await _context.CarDamageReports
                .Include(r => r.Car)
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<CarDamageReport?> CreateAsync(CarDamageReport report)
        {
            try
            {
                _context.CarDamageReports.Add(report);
                await _context.SaveChangesAsync();
                return report;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CarDamageReport?> UpdateAsync(CarDamageReport report)
        {
            try
            {
                var existing = await _context.CarDamageReports.FindAsync(report.Id);
                if (existing == null) return null;

                existing.CarId = report.CarId;
                existing.EmployeeId = report.EmployeeId;
                existing.ReportDate = report.ReportDate;
                existing.Description = report.Description;
                existing.DamageType = report.DamageType;
                existing.PhotoUrl = report.PhotoUrl;
                existing.IsResolved = report.IsResolved;
                existing.ResolvedAt = report.ResolvedAt;

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
                var report = await _context.CarDamageReports.FindAsync(id);
                if (report == null) return false;

                _context.CarDamageReports.Remove(report);
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
