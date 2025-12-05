using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository repository;

        public BasketController(IBasketRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await repository.GetBasketAsync(id);

            if (basket == null)
            {
                basket = new CustomerBasket(id);
                await repository.UpdateBasketAsync(basket);
            }

            return Success(basket);
        }
        [HttpPut]
        public async Task<ActionResult<CustomerBasket>> UpdateOrAddBasket([FromBody] CustomerBasket basket)
        {
            var updatedBasket = await repository.UpdateBasketAsync(basket);
            if (updatedBasket == null)
                return Error("Problem updating the basket", StatusCodes.Status400BadRequest);
            return Success(updatedBasket);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await repository.DeleteBasketAsync(id);
            return NoContent();


        }

    }
}
