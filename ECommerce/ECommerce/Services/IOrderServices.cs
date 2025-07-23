using ECommerce.Dtos.Category;
using ECommerce.Dtos.Order;

namespace ECommerce.Services
{
    public interface IOrderServices
    {
        Task<OrderDetailsDto> CreateOrderAsync(OrderCreateDto orderDto);
        //Task<List<CategoryDto>> GetAllOrders();
        Task<OrderDetailsDto> GetOrderByIdAsync(int id);
        Task<OrderDetailsDto> DeleteOrderById(int id);
        Task<OrderDetailsDto> UpdateOrderById(int id);
    }
}
