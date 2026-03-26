using E_CommerceOrderManagementAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceOrderManagementAPI.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        //Independent
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        //has 1-n relationship with customers 1 customer can have multiple oreders
        public DbSet<Order> Orders { get; set; }
        // has 1-n relationship with orders 1 order can have multiple order items
        public DbSet<OrderItem> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerID)
                .HasPrincipalKey(e => e.CustomerID);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItem)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderID)
                .HasPrincipalKey(i => i.OrderID);

            modelBuilder.Entity<Order>()
                .Navigation(O => O.OrderItem)
                .HasField("_orderItem");

            modelBuilder.Entity<Order>()
                .Ignore(o => o.Total);

            modelBuilder.Entity<OrderItem>()
                .Ignore(i => i.LineTotal);

        }

    }
}
