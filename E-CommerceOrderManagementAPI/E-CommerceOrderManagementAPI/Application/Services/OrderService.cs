using E_CommerceOrderManagementAPI.Application.Interfaces;
using E_CommerceOrderManagementAPI.Contracts.Requests;
using E_CommerceOrderManagementAPI.Contracts.Responses;
using E_CommerceOrderManagementAPI.Domain.Entities;
using E_CommerceOrderManagementAPI.Domain.Exceptions;

namespace E_CommerceOrderManagementAPI.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IPaymentGateway _paymentGateway;
        private readonly INotificationService _notificationService;
        public OrderService(IOrderRepository orderRepository, IInventoryService inventoryService, IPaymentGateway paymentGateway, INotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _inventoryService = inventoryService;
            _paymentGateway = paymentGateway;
            _notificationService = notificationService;
        }
        public async Task AddOrderItem(Guid orderId, AddOrderItemRequest orderItemDto)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new DomainException($"Order with order ID {orderId} not found");
            Product product = await _inventoryService.GetProductByIdAsync(orderItemDto.ProductID);
            OrderItem orderItem = new(product.ProductID, order.OrderID, product.ProductName, product.UnitPrice, product.ListPrice);
            orderItem.SetQty(orderItemDto.Qty);
            order.AddItem(orderItem);
            await _orderRepository.UpdateOrder(order);
        }

        public async Task CancelOrderAsync(Guid orderId)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new DomainException($"Order with order ID {orderId} not found");
            order.CancelOrder();
            await _orderRepository.UpdateOrder(order);
        }

        public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest orderDto)
        {
            OrderResponse response = new();
            Order newOrder = new(orderDto.CustomerID);
            foreach (var item in orderDto.OrderedItems)
            {
                // create orderItems;
                Product product = await _inventoryService.GetProductByIdAsync(item.ProductID);
                OrderItem orderItem = new(product.ProductID,newOrder.OrderID, product.ProductName, product.UnitPrice, product.ListPrice);
                orderItem.SetQty(item.Qty);
                newOrder.AddItem(orderItem);
            }
            bool paymentSuccess = await _paymentGateway.ProcessPayment(newOrder.OrderID, newOrder.Total);
            if(paymentSuccess) 
                newOrder.MarkAsPaid();
            else 
                throw new DomainException($"Payment failed for order ID {newOrder.OrderID}");
            //Needs Order and this should return and OrderResponse
            bool orderCreated = await _orderRepository.AddOrder(newOrder);
            if (orderCreated) 
            {
                await _notificationService.GenerateOrderNotification(newOrder.OrderID, newOrder.CustomerID, newOrder.Status.ToString(), $"Order Confirmed with order ID {newOrder.OrderID}");
                response = MapToResponse(newOrder);
            }
            else 
                throw new DomainException($"Failed to create order for customer ID {orderDto.CustomerID}");
            return response;
        }

        public async Task<OrderResponse> GetOrderByIdAsync(Guid orderId)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new DomainException($"Order with order ID {orderId} not found");
           
            return MapToResponse(order);
        }

        private OrderResponse MapToResponse(Order order) 
        {
            OrderResponse response = new()
            {
                OrderID = order.OrderID,
                OrderStatus = order.Status.ToString(),
                PaymentStatus = order.PaymentStatus.ToString(),
                Total = order.Total,
                OrderedItems = [.. order.OrderItem.Select(oi => new OrderItemResponse
                {
                    ProductName = oi.Name,
                    Qty = oi.Qty,
                    ListPrice = oi.ListPrice,
                    LinePrice = oi.LineTotal
                })]
            };
            return response;
        }
    }
}
