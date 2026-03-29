using System.ComponentModel.DataAnnotations;

namespace E_CommerceOrderManagementAPI.Domain.Entities
{
    public class Customer
    {
        [Key]
        public Guid CustomerID { get; private set; } = Guid.NewGuid();

        [Required]
        public required string CustomerFirstName { get; set; }
        [Required]
        public required string CustomerLastName { get; set; }
        [Required]
        public required string Address { get; set; }

        public string Address1 { get; set; } = string.Empty;
        [Required]
        public required string PhoneNumber { get; set; }
        [Required]
        public required string City { get; set; }
        [Required]
        public required string State { get; set; }
        [Required]
        public required string Country { get; set; }
        [Required]
        public required string Zip { get; set; }

        public ICollection<Order> Orders { get; private set; } = [];
    }
}
