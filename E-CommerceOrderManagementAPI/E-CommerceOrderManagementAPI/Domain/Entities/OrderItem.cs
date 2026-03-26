using E_CommerceOrderManagementAPI.Domain.Exceptions;

namespace E_CommerceOrderManagementAPI.Domain.Entities
{
    public class OrderItem
    {
        public Guid ProductID { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int Qty 
        {
            get;
            private set;
        }
        public decimal UnitPrice { get; private set; }
        public decimal ListPrice { get; private set; }
        public decimal LineTotal 
        {
            get => Qty * ListPrice;
        }

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
