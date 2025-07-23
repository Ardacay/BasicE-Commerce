using ECommerceManager.Dtos.Categories;

namespace ECommerceManager.Managers
{
    public interface ICategoryManager
    {

        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryDto dto);
        Task<CategoryDto> UpdateAsync(CategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
