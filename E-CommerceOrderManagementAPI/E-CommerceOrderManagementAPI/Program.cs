using E_CommerceOrderManagementAPI.Application.Interfaces;
using E_CommerceOrderManagementAPI.Application.Services;
using E_CommerceOrderManagementAPI.Domain.Exceptions;
using E_CommerceOrderManagementAPI.Infrastructure.Notifications;
using E_CommerceOrderManagementAPI.Infrastructure.Payments;
using E_CommerceOrderManagementAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<INotificationService, EmailNotificationService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentGateway, FakePaymentGateway>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception is DomainException ? 400 : 500;
        await context.Response.WriteAsJsonAsync(new { error = exception?.Message });
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
