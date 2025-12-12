using Application.DTOs;
using Domain.Entities.Identity;

namespace Application.Services.Contract
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shipToAddress);
        Task<IReadOnlyList<OrderDto>> GetOrdersForUserAsync(string buyerEmail);
        Task<OrderDto> GetOrderByIdAsync(int orderId, string buyerEmail);
    }

    
}
