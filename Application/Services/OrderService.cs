using Application.DTOs.Order;
using Application.Services.Contract;
using Domain.Contracts;
using Domain.Entities.Identity;

namespace Application.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public Task<ReadOrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            // get basket from basket repo
            // get select items at basket from product repo
            // calc subtotal
            // get delivery method from deliveryMethod repo
            // create order
            // save to db
        }

        public Task<ReadOrderDto> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ReadOrderDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
