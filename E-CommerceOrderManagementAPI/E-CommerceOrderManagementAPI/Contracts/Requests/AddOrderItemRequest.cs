namespace E_CommerceOrderManagementAPI.Contracts.Requests
{
    public record AddOrderItemRequest
    {
        public Guid ProductID { get; set; }

        public int Qty { get; set; }

    }
}
