using E_CommerceOrderManagementAPI.Domain.Enums;
using E_CommerceOrderManagementAPI.Domain.Exceptions;

namespace E_CommerceOrderManagementAPI.Domain.Entities
{
    public class Order
    {
        private List<OrderItem> _orderItems = [];

        public Guid OrderID 
        {
            get;
            private set;
        } = Guid.NewGuid();

        public OrderStatus Status { get; private set; }

        public PaymentStatus PaymentStatus { get; private set; }

        public Guid CustomerID { get; private set; }

        public IReadOnlyCollection<OrderItem> OrderItem => _orderItems.AsReadOnly();

        //finds linetotal from the order items and sums them up to get the total for the order
        public decimal Total => _orderItems.Sum(item => item.LineTotal);

        public Order(Guid customerID)
        {
            if(customerID == Guid.Empty)
                throw new DomainException("Customer ID cannot be empty.");
            CustomerID = customerID;
        }

        public void AddItem(OrderItem newOrder) 
        {
            //check if the item already exists in the order, if it does then we update the quantity and price
            if (this.Status is OrderStatus.Cancelled)
                throw new DomainException("Cannot add items to a cancelled order.");
            bool itemExists = _orderItems.Any(item => item.ProductID == newOrder.ProductID);
            if (itemExists)
            {
                _orderItems.FirstOrDefault(item => item.ProductID == newOrder.ProductID)?.SetQty(newOrder.Qty);
            }
            else
                _orderItems.Add(newOrder);
        }

        public void RemoveItem(Guid productID) 
        {
            bool itemExists = _orderItems.Any(item => item.ProductID == productID);
            if(itemExists)
                _orderItems.RemoveAll(item => item.ProductID == productID);
            else
                throw new DomainException("Item not found in the order.");
        }

        public void CancelOrder() 
        {
            if (this.Status is not OrderStatus.Cancelled)
                this.Status = OrderStatus.Cancelled;
        }

        public void UpdatePaymentStatus(PaymentStatus newStatus) 
        {
            if (_orderItems.Count == 0)
                throw new DomainException("Cannot pay for an empty order.");
            if (this.Status is OrderStatus.Cancelled)
                throw new DomainException("Cannot update payment status of a cancelled order.");
            this.PaymentStatus = newStatus;
        }

        public void UpdateOrderStatus(OrderStatus newStatus) 
        {
            if (this.Status is OrderStatus.Cancelled)
                throw new DomainException("Cannot update status of a cancelled order.");
            this.Status = newStatus;
        }

        public void MarkAsPaid()
        {
            if (_orderItems.Count == 0)
                throw new DomainException("Cannot pay for an empty order.");
            if (Status == OrderStatus.Cancelled)
                throw new DomainException("Cancelled order cannot be paid.");
            Status = OrderStatus.Paid;
            PaymentStatus = PaymentStatus.Paid;
        }
    }
}
