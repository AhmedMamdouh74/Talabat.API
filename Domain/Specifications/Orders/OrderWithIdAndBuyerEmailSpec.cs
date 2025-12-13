using Domain.Entities.Order_Aggregate;

namespace Domain.Specifications.Orders
{
    public class OrderWithIdAndBuyerEmailSpec : BaseSpecification<Order>
    {
        public OrderWithIdAndBuyerEmailSpec(int id, string buyerEmail) : base(o => o.Id == id && o.BuyerEmail == buyerEmail)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
