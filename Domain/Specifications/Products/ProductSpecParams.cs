

using System.Reflection.Metadata.Ecma335;

namespace Domain.Specifications.Products
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 10;
        private string? search;


        public int PageIndex { get; set; } = 1;
        private int pageSize = 5;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? Search { get => search; set => search = value?.ToLower(); }
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
    }
}
