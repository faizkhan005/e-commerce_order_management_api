using E_CommerceOrderManagementAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace E_CommerceOrderManagementAPI.Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        //has 1-n relationship with customers 1 customer can have multiple oreders
        public DbSet<Order> Orders { get; set; }
        // has 1-n relationship with orders 1 order can have multiple order items
        public DbSet<OrderItem> OrderItems { get; set; }

        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        int SaveChanges();
    }
}
