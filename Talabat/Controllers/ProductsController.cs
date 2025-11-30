using Application.DTOs.Product;
using AutoMapper;
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
    public class ProductsController : BaseAPIController
    {
        private readonly IGenericRepository<Product> repository;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ReadProductDto>>> GetProducts([FromQuery] string? sort,int? brandId,int? categoryId)
        {

            var spec = new ProductWithCategoryAndBrandSpec(sort,brandId,categoryId);
            var products = await repository.GetAllWithSpecAsync(spec);
            if (products == null || !products.Any())
                return Error("resourses not found", StatusCodes.Status404NotFound);
            var records = mapper.Map<IReadOnlyList<ReadProductDto>>(products);
            return Success(records);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReadProductDto>> GetProductById( int id)
        {
         
            var spec = new ProductWithCategoryAndBrandSpec(id);
            var product = await repository.GetByIdWithSpecAsync(spec);
            
            if (product == null)
                return Error("resourse not found", StatusCodes.Status404NotFound);
            var record = mapper.Map<ReadProductDto>(product);
            return Success(record);
        }
    }
}
