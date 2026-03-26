using E_CommerceOrderManagementAPI.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceOrderManagementAPI.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        public Guid ProductID { get; private set; }
        public Guid OrderID { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int Qty 
        {
            get;
            private set;
        }
        public decimal UnitPrice { get; private set; }
        public decimal ListPrice { get; private set; }
        [NotMapped]
        public decimal LineTotal 
        {
            get => Qty * ListPrice;
        }
        //Navigation Property for EFCore
        public Order Order { get; set; }
        public void SetQty(int newQty) 
        {
            if(newQty < 0)
                throw new DomainException("Quantity cannot be negative.");
            Qty += newQty;
        }

        public OrderItem(Guid productID, string name, decimal unitPrice, decimal listPrice)
        {
            ProductID = productID;
            Name = name;
            UnitPrice = unitPrice;
            ListPrice = listPrice;
        }

    }
}
