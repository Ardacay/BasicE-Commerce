using ECommerce.Dtos.Category;
using ECommerce.Dtos.Order;

namespace ECommerce.Services
{
    public interface IOrderServices
    {
        Task<OrderDto> CreateOrderAsync(OrderDto dto);
        Task<List<OrderDto>> GetAllOrders();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<OrderDto> DeleteOrderById(int id);
        Task<OrderDto> UpdateOrder(OrderDto dto);
    }
}
