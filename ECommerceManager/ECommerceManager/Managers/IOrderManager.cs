using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Order;

namespace ECommerceManager.Managers
{
    public interface IOrderManager
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<OrderDto> CreateAsync(OrderDto dto);
        Task<OrderDto> UpdateAsync(OrderDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
