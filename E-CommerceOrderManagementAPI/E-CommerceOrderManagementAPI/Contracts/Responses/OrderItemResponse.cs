namespace E_CommerceOrderManagementAPI.Contracts.Responses
{
    public record OrderItemResponse
    {
        public required string ProductName { get; set; }

        public int Qty { get; set; }

        public decimal ListPrice { get; set; }

        public decimal LinePrice { get; set; }
    }
}
