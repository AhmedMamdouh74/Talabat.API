using Application.DTOs.Order;
using Application.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseAPIController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<ReadOrderDto>> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            var order = await orderService.CreateOrderAsync(orderDto);
            return Success(order);
        }
    }
}
