using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Order;

namespace ECommerceManager.Managers
{
    public interface IOrderManager
    {
        Task<List<OrderDto>> GetAllAsync();
        Task<OrderDetailsDto> GetByIdAsync(int id);
        Task<OrderDetailsDto> CreateAsync(OrderCreateDto dto);
        //Task<CategoryDto> UpdateAsync(CategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
