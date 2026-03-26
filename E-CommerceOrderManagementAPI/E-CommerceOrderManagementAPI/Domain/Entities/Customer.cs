namespace E_CommerceOrderManagementAPI.Domain.Entities
{
    public class Customer
    {
        public Guid CustomerID { get; private set; } = Guid.NewGuid();

        public required string CustomerFirstName { get; set; }
        public required string CustomerLastName { get; set; }

        public required string Address { get; set; }

        public string Address1 { get; set; } = string.Empty;

        public required string PhoneNumber { get; set; }

        public required string City { get; set; }

        public required string State { get; set; }

        public required string Country { get; set; }

        public required string Zip { get; set; }

    }
}
