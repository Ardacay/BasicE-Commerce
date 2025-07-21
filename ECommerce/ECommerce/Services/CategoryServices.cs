using AutoMapper;
using ECommerce.Dtos.Category;
using ECommerce.Dtos.Order;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryServices(IRepository<Product> productRepository, IRepository<Order> orderRepository, IRepository<Category> categoryrepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _categoryRepository = categoryrepository;
            _mapper = mapper;
        }

        public CategoryDto CreateCategory(CategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            try
            {
                _categoryRepository.Add(entity);
                _categoryRepository.Save();

            }
            catch (Exception ex)
            {

            }
            return dto;
        }

        public async Task<CategoryDto> DeleteCategory(int id)
        {
            var categories =   await _categoryRepository.GetIdAsync(id);
            if (categories == null) throw new Exception("Category not found");
            _categoryRepository.Remove(categories);
            _categoryRepository.Save();
            return _mapper.Map<CategoryDto>(categories);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var order = await _categoryRepository.GetIdAsync(id);
            return _mapper.Map<CategoryDto>(id);
        }

        public async Task<CategoryDto> UpdateCategory(int id)
        {
            var categories = await _categoryRepository.GetIdAsync(id);
            if (categories == null) throw new Exception("Category Not Found");
            _categoryRepository.Update(categories);
            _categoryRepository.Save();
            return _mapper.Map<CategoryDto>(categories);
        }
    }
}
//cascading