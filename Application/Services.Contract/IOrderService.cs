using Application.DTOs.Order;

namespace Application.Services.Contract
{
    public interface IOrderService
    {
        Task<ReadOrderDto> CreateOrderAsync(CreateOrderDto orderDto);
        Task<IReadOnlyList<ReadOrderDto>> GetOrdersForUserAsync(string buyerEmail);
        Task<ReadOrderDto> GetOrderByIdAsync(int orderId, string buyerEmail);
    }

    
}
