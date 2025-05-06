using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgres
{
    public class AppDbContext : DbContext
    {
        public DbSet<Blacklist> Blacklists { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<CarDamageReport> CarDamageReports { get; set; }
        public DbSet<CarDocument> CarDocuments { get; set; }
        public DbSet<CarLocation> CarLocations { get; set; }
        public DbSet<CarRentHistory> CarRentHistory { get; set; }
        public DbSet<CarReview> CarReviews { get; set; }
        public DbSet<CarStatus> CarStatuses { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentsTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    }
}
