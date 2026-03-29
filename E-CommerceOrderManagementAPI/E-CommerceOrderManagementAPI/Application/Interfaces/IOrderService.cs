using E_CommerceOrderManagementAPI.Contracts.Requests;
using E_CommerceOrderManagementAPI.Contracts.Responses;

namespace E_CommerceOrderManagementAPI.Application.Interfaces
{
    public interface IOrderService
    {

        public Task CreateOrderAsync(CreateOrderRequest orderDto);

        public Task<OrderResponse> GetOrderByIdAsync(Guid orderId);

        public Task CancelOrderAsync(Guid orderId);

        public Task AddOrderItem(Guid orderId, AddOrderItemRequest orderItemDto);
    }
}
