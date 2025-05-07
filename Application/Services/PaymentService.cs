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

    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Payment>?> GetAllAsync()
        {
            return await _context.Payments.Include(p => p.Order)
                                          .Include(p => p.Customer)
                                          .Include(p => p.PaymentType)
                                          .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments.Include(p => p.Order)
                                          .Include(p => p.Customer)
                                          .Include(p => p.PaymentType)
                                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Payment?> CreateAsync(Payment payment)
        {
            try
            {
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return payment;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Payment?> UpdateAsync(Payment payment)
        {
            try
            {
                var existing = await _context.Payments.FindAsync(payment.Id);
                if (existing == null) return null;

                existing.OrderId = payment.OrderId;
                existing.CustomerId = payment.CustomerId;
                existing.Amount = payment.Amount;
                existing.PaymentTypeId = payment.PaymentTypeId;
                existing.PaymentDate = payment.PaymentDate;

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
                var payment = await _context.Payments.FindAsync(id);
                if (payment == null) return false;

                _context.Payments.Remove(payment);
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
