using Application.DTOs.Product;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Specifications;
using Domain.Specifications.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Responses;

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
        public async Task<ActionResult<PagedResult<ReadProductDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {

            var spec = new ProductWithCategoryAndBrandSpec(specParams);
            var products = await repository.GetAllWithSpecAsync(spec);
            if (products == null || !products.Any())
                return Error("resourses not found", StatusCodes.Status404NotFound);
            var filteredSpec = new ProductWithFilterationAndCountSpec(specParams);
            var count=await repository.GetCountWithSpecAsync(filteredSpec);
            var records = mapper.Map<IReadOnlyList<ReadProductDto>>(products);
            var pagedResult = new PagedResult<ReadProductDto>(specParams.PageIndex, specParams.PageSize, count)
            {
                Items = records
            };
            return Success(pagedResult);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReadProductDto>> GetProductById(int id)
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
