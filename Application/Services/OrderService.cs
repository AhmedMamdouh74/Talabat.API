using Application.DTOs;
using Application.Services.Contract;
using Domain.Entities.Identity;

namespace Application.Services
{
    internal class OrderService : IOrderService
    {
        public Task<int> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shipToAddress)
        {
            // get basket from basket repo
            // get select items at basket from product repo
            // calc subtotal
            // get delivery method from deliveryMethod repo
            // create order
            // save to db
        }

        public Task<OrderDto> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<OrderDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
