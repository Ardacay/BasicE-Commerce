using AutoMapper;
using ECommerce.Controllers;
using ECommerce.Dtos.Product;
using ECommerce.Models;
using ECommerce.Repositories;
using Humanizer;

namespace ECommerce.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductServices(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
           
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateProductAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            try
            {
                _productRepository.Add(product);
            }
            catch (Exception e)
            {

            }
            _productRepository.Save();

            return _mapper.Map<ProductDto>(product);


        }

        public async Task<ProductDto> DeleteProduct(int id)
        {
            var product = await _productRepository.GetIdAsync(id);
            if (product == null) throw new Exception("Product Not Found");
            _productRepository.Remove(product);
            _productRepository.Save();
            return _mapper.Map<ProductDto>(product);
        }

        //public async Task<List<ProductDto>> GetAllProductsAsync()
        //{
        //    var products = await _productRepository.GetAllAsync();
        //    return _mapper.Map<List<ProductDto>>(products);

        //}

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetIdAsync(id);
            return _mapper.Map<ProductDto?>(product);
        }

        public  async Task<ProductDto> UpdateProduct(ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            if (product == null) throw new Exception("Product Not Found");
           _productRepository.Update(product);
            _productRepository.Save();
            return _mapper.Map<ProductDto>(product);

        }
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllWithIncludeAsync(p => p.Category);
            return _mapper.Map<List<ProductDto>>(products);
        }

    }
}
