using Domain.Entities;

namespace Domain.Specifications.Products
{
    public class ProductWithCategoryAndBrandSpec : BaseSpecification<Product>
    {
        public ProductWithCategoryAndBrandSpec()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
           
        }
    }
}
