namespace Domain.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order(string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem>? orderItems, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public Order()
        {
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; } = new HashSet<OrderItem>();
        public decimal Subtotal { get; set; }
        // for derived attribute, not mapped to DB
        public decimal GetTotal()
        {
            return Subtotal + (DeliveryMethod?.Cost ?? 0);
        }
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
