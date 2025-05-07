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

    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly AppDbContext _context;

        public PaymentTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentType>?> GetAllAsync()
        {
            return await _context.PaymentsTypes.Include(pt => pt.Payments).ToListAsync();
        }

        public async Task<PaymentType?> GetByIdAsync(int id)
        {
            return await _context.PaymentsTypes.Include(pt => pt.Payments)
                                              .FirstOrDefaultAsync(pt => pt.Id == id);
        }

        public async Task<PaymentType?> CreateAsync(PaymentType paymentType)
        {
            try
            {
                _context.PaymentsTypes.Add(paymentType);
                await _context.SaveChangesAsync();
                return paymentType;
            }
            catch
            {
                return null;
            }
        }

        public async Task<PaymentType?> UpdateAsync(PaymentType paymentType)
        {
            try
            {
                var existing = await _context.PaymentsTypes.FindAsync(paymentType.Id);
                if (existing == null) return null;

                existing.Name = paymentType.Name;

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
                var paymentType = await _context.PaymentsTypes.FindAsync(id);
                if (paymentType == null) return false;

                _context.PaymentsTypes.Remove(paymentType);
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
