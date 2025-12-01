using Domain.Entities;

namespace Domain.Specifications.Products
{
    public class ProductWithCategoryAndBrandSpec : BaseSpecification<Product>
    {
        public ProductWithCategoryAndBrandSpec(ProductSpecParams specParams) : base(p =>
        (string.IsNullOrEmpty(specParams.Search) || p.Name.Contains(specParams.Search))
        && (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId)
        && (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId))
        {
            AddIncludes();
            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
            if (string.IsNullOrEmpty(specParams.Sort))
            {
                AddOrderByAsc(p => p.Name);
                return;
            }
            else switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderByAsc(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderByAsc(p => p.Name);
                        break;
                }


        }



        public ProductWithCategoryAndBrandSpec(int id) : base(p => p.Id == id)
        {
            AddIncludes();
        }
        private void AddIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
