using E_CommerceOrderManagementAPI.Domain.Enums;

namespace E_CommerceOrderManagementAPI.Contracts.Responses
{
    public record OrderResponse
    {
        public Guid OrderID { get; set; }

        public string OrderStatus { get; set; }

        public string PaymentStatus { get; set; }

        public List<OrderItemResponse> OrderedItems { get; set; }

        public decimal Total { get; set; }
    }
}
