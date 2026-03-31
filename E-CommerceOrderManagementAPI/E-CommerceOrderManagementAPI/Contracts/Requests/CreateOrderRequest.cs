namespace E_CommerceOrderManagementAPI.Contracts.Requests
{
    public record CreateOrderRequest
    {
        public Guid CustomerID { get; set; }

        public List<AddOrderItemRequest> OrderdedItems { get; set; } = [];
    }
}
