using Application.Interfaces;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Postgres;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBlacklistService, BlacklistService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarCategoryService, CarCategoryService>();
builder.Services.AddScoped<ICarDamageReportService, CarDamageReportService>();
builder.Services.AddScoped<ICarDocumentService, CarDocumentService>();
builder.Services.AddScoped<ICarLocationService, CarLocationService>();
builder.Services.AddScoped<ICarRentHistoryService, CarRentHistoryService>();
builder.Services.AddScoped<ICarReviewService, CarReviewService>();
builder.Services.AddScoped<ICarStatusService, CarStatusService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRoleService, EmployeeRoleService>();
builder.Services.AddScoped<IInsuranceService, InsuranceService>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserStatusService, UserStatusService>();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();