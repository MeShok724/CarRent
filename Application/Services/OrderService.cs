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

    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>?> GetAllAsync()
        {
            return await _context.Orders.Include(o => o.Customer)
                                        .Include(o => o.Car)
                                        .Include(o => o.BranchFrom)
                                        .Include(o => o.BranchTo)
                                        .Include(o => o.Employee)
                                        .Include(o => o.Discount)
                                        .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.Customer)
                                        .Include(o => o.Car)
                                        .Include(o => o.BranchFrom)
                                        .Include(o => o.BranchTo)
                                        .Include(o => o.Employee)
                                        .Include(o => o.Discount)
                                        .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> CreateAsync(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Order?> UpdateAsync(Order order)
        {
            try
            {
                var existing = await _context.Orders.FindAsync(order.Id);
                if (existing == null) return null;

                existing.CustomerId = order.CustomerId;
                existing.CarId = order.CarId;
                existing.BranchFromId = order.BranchFromId;
                existing.BranchToId = order.BranchToId;
                existing.EmployeeId = order.EmployeeId;
                existing.StartDate = order.StartDate;
                existing.ReturnDate = order.ReturnDate;
                existing.PriceTotal = order.PriceTotal;
                existing.DiscountId = order.DiscountId;
                existing.Status = order.Status;
                existing.Notes = order.Notes;
                existing.CreatedAt = order.CreatedAt;

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
                var order = await _context.Orders.FindAsync(id);
                if (order == null) return false;

                _context.Orders.Remove(order);
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
