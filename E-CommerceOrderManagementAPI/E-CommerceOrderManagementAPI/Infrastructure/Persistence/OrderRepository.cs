using E_CommerceOrderManagementAPI.Application.Interfaces;
using E_CommerceOrderManagementAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceOrderManagementAPI.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IAppDbContext _context;
        public OrderRepository(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOrder(Order newOrder)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try 
            {
                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex) 
            {
                //TODO: Log the exception
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders.Include(i=>i.OrderItem).FirstOrDefaultAsync(f => f.OrderID == orderId);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            try 
            {
                int rows = await _context.SaveChangesAsync();
                if (rows > 0)
                    return true;
                return false;
            }
            catch(Exception ex) 
            {
                //TODO: Log the exception
                return false;
            }
        }
    }
}
