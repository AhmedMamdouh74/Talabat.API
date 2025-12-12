namespace Domain.Entities.Order_Aggregate
{
    public class ProductOrderedItem
    {
        public ProductOrderedItem()
        {
        }

        public ProductOrderedItem(int productItemId, string? productName, string? pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

    }
}
