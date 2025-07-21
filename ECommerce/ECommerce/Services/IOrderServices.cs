using ECommerce.Dtos.Order;

namespace ECommerce.Services
{
    public interface IOrderServices
    {
        Task<OrderDetailsDto> CreateOrderAsync(OrderCreateDto orderDto);
        Task<OrderDetailsDto> GetOrderByIdAsync(int id);
        Task<OrderDetailsDto> DeleteOrderById(int id);
        Task<OrderDetailsDto> UpdateOrderById(int id);
    }
}
