using ECommerceManager.Dtos.Categories;

namespace ECommerceManager.İnterfaces
{
    public interface IManager<TDto>
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
