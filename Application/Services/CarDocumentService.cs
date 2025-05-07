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

    public class CarDocumentService : ICarDocumentService
    {
        private readonly AppDbContext _context;

        public CarDocumentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarDocument>?> GetAllAsync()
        {
            return await _context.CarDocuments
                .Include(d => d.Car)
                .ToListAsync();
        }

        public async Task<CarDocument?> GetByIdAsync(int id)
        {
            return await _context.CarDocuments
                .Include(d => d.Car)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<CarDocument?> CreateAsync(CarDocument document)
        {
            try
            {
                _context.CarDocuments.Add(document);
                await _context.SaveChangesAsync();
                return document;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CarDocument?> UpdateAsync(CarDocument document)
        {
            try
            {
                var existing = await _context.CarDocuments.FindAsync(document.Id);
                if (existing == null) return null;

                existing.CarId = document.CarId;
                existing.Type = document.Type;
                existing.Number = document.Number;
                existing.IssueDate = document.IssueDate;
                existing.ExpirationDate = document.ExpirationDate;
                existing.FileUrl = document.FileUrl;

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
                var document = await _context.CarDocuments.FindAsync(id);
                if (document == null) return false;

                _context.CarDocuments.Remove(document);
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
