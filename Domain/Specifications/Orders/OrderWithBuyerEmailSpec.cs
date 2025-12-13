using Domain.Entities.Order_Aggregate;

namespace Domain.Specifications.Orders
{
    public class OrderWithBuyerEmailSpec : BaseSpecification<Order>
    {
        public OrderWithBuyerEmailSpec(string buyerEmail) : base(o => o.BuyerEmail == buyerEmail)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
