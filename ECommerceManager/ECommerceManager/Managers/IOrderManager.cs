using ECommerceManager.Dtos.Order;

namespace ECommerceManager.Managers
{
    public interface IOrderManager
    {
        Task<List<OrderDetailsDto>> GetAllAsync();
        Task<OrderDetailsDto> GetByIdAsync(int id);
        Task<OrderDetailsDto> CreateAsync(OrderCreateDto dto);
    }
}
