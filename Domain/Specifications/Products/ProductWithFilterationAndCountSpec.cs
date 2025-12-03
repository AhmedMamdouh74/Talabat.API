using Domain.Entities;

namespace Domain.Specifications.Products
{
    public class ProductWithFilterationAndCountSpec : BaseSpecification<Product>
    {
        public ProductWithFilterationAndCountSpec(ProductSpecParams productSpec) : base(p =>
            (string.IsNullOrEmpty(productSpec.Search) || p.Name.Contains(productSpec.Search)) &&
            (!productSpec.BrandId.HasValue || p.BrandId == productSpec.BrandId) &&
            (!productSpec.CategoryId.HasValue || p.CategoryId == productSpec.CategoryId)
            )
        {

        }

    }
}
