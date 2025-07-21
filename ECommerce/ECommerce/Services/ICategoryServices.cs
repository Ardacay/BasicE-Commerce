using ECommerce.Dtos.Category;
using ECommerce.Dtos.Order;

namespace ECommerce.Services
{
    public interface ICategoryServices
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        CategoryDto CreateCategory(CategoryDto dto);

        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> DeleteCategory(int id);
        Task<CategoryDto> UpdateCategory(int id);
    }
}
