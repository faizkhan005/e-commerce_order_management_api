using E_CommerceOrderManagementAPI.Domain.Entities;

namespace E_CommerceOrderManagementAPI.Application.Interfaces
{
    public interface IOrderRepository
    {
        public Task<Order?> GetOrderByIdAsync(Guid orderId);

        public Task<bool> AddOrder(Order newOrder);

        public Task<bool> UpdateOrder(Order order);

    }
}
