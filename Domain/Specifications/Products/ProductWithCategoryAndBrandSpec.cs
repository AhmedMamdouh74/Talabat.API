using Domain.Entities;

namespace Domain.Specifications.Products
{
    public class ProductWithCategoryAndBrandSpec : BaseSpecification<Product>
    {
        public ProductWithCategoryAndBrandSpec(string sort)
        {
            AddIncludes();
            if (string.IsNullOrEmpty(sort))
            {
                AddOrderByAsc(p => p.Name);
                return;
            }
            else switch (sort)
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
