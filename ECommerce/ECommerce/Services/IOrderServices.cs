using ECommerce.Dtos.Order;

namespace ECommerce.Services
{
    public interface IOrderServices
    {
        Task<OrderDetailsDto> CreateOrderAsync(OrderCreateDto orderDto);
        Task<OrderDetailsDto> GetOrderByIdAsync(int id);
    }
}
