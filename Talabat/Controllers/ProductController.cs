using Domain.Concrats;
using Domain.Entities;
using Domain.Specifications;
using Domain.Specifications.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseAPIController
    {
        private readonly IGenericRepository<Product> repository;

        public ProductController(IGenericRepository<Product> _repository)
        {
            repository = _repository;
        }
        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts()
        {
            var spec=new ProductWithCategoryAndBrandSpec();
            var products = await repository.GetAllWithSpecAsync(spec);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductWithCategoryAndBrandSpec(id);
            var product = await repository.GetByIdWithSpecAsync(spec);
            return Ok(product);
        }
    }
}
