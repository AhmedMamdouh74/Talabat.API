using Application.DTOs.Order;
using Domain.Entities.Identity;

namespace Application.Services.Contract
{
    public interface IOrderService
    {
        Task<CreateOrderDto> CreateOrderAsync(CreateOrderDto orderDto);
        Task<IReadOnlyList<CreateOrderDto>> GetOrdersForUserAsync(string buyerEmail);
        Task<CreateOrderDto> GetOrderByIdAsync(int orderId, string buyerEmail);
    }

    
}
