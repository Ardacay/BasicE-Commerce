using ECommerceManager.Dtos.Product;

namespace ECommerceManager.Managers
{
    public interface IProductManager
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductCreateDto dto);
        Task<ProductDto> UpdateAsync(ProductDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
