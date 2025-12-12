namespace Domain.Entities.Order_Aggregate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductOrderedItem? _product, decimal _price, int _quantity)
        {
            Product = _product;
            Price = _price;
            Quantity = _quantity;
        }

        public ProductOrderedItem Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
    }
}
