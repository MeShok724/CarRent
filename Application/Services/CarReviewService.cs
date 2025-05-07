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

    public class CarReviewService : ICarReviewService
    {
        private readonly AppDbContext _context;

        public CarReviewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarReview>?> GetAllAsync()
        {
            return await _context.CarReviews
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .Include(r => r.Order)
                .ToListAsync();
        }

        public async Task<CarReview?> GetByIdAsync(int id)
        {
            return await _context.CarReviews
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .Include(r => r.Order)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<CarReview?> CreateAsync(CarReview review)
        {
            try
            {
                _context.CarReviews.Add(review);
                await _context.SaveChangesAsync();
                return review;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CarReview?> UpdateAsync(CarReview review)
        {
            try
            {
                var existing = await _context.CarReviews.FindAsync(review.Id);
                if (existing == null) return null;

                existing.CarId = review.CarId;
                existing.CustomerId = review.CustomerId;
                existing.OrderId = review.OrderId;
                existing.Rating = review.Rating;
                existing.ReviewText = review.ReviewText;
                existing.AddedAt = review.AddedAt;

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
                var review = await _context.CarReviews.FindAsync(id);
                if (review == null) return false;

                _context.CarReviews.Remove(review);
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
