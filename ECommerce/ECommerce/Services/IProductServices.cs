using ECommerce.Dtos.Product;

namespace ECommerce.Services
{
    public interface IProductServices
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductCreateDto dto);
        Task<ProductDto> UpdateProduct(int id);
        Task<ProductDto> DeleteProduct(int id);

    }
}
