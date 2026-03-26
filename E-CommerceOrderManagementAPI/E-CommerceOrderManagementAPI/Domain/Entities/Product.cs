namespace E_CommerceOrderManagementAPI.Domain.Entities
{
    public class Product
    {
        public Guid ProductID { get; private set; } = Guid.NewGuid();

        public required string ProductName { get; set; }

        public required string ProductDescription { get; set; }
        public decimal UnitPrice { get; private set; }

        public decimal ListPrice { get; private set; }

        //TODO: Move it to enum
        public required string ProductCategory { get; set; }

        public decimal DiscountPercentage { get; private set; }


    }
}
