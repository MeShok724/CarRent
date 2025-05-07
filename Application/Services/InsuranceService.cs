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

    public class InsuranceService : IInsuranceService
    {
        private readonly AppDbContext _context;

        public InsuranceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Insurance>?> GetAllAsync()
        {
            return await _context.Insurance.ToListAsync();
        }

        public async Task<Insurance?> GetByIdAsync(int id)
        {
            return await _context.Insurance.FindAsync(id);
        }

        public async Task<Insurance?> CreateAsync(Insurance insurance)
        {
            try
            {
                _context.Insurance.Add(insurance);
                await _context.SaveChangesAsync();
                return insurance;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Insurance?> UpdateAsync(Insurance insurance)
        {
            try
            {
                var existing = await _context.Insurance.FindAsync(insurance.Id);
                if (existing == null) return null;

                existing.PolicyNumber = insurance.PolicyNumber;
                existing.CompanyName = insurance.CompanyName;
                existing.InsuranceType = insurance.InsuranceType;
                existing.IssueDate = insurance.IssueDate;
                existing.ExpirationDate = insurance.ExpirationDate;
                existing.FileUrl = insurance.FileUrl;

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
                var insurance = await _context.Insurance.FindAsync(id);
                if (insurance == null) return false;

                _context.Insurance.Remove(insurance);
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
