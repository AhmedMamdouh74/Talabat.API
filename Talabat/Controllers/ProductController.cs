using Domain.Concrats;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseAPIController
    {
        private readonly IGenericRepository<Product, int> repository;

        public ProductController(IGenericRepository<Product, int> _repository)
        {
            repository = _repository;
        }
        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts()
        {
            var products = await repository.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await repository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
