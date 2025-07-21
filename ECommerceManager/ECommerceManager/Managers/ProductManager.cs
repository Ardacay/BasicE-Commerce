using ECommerceManager.Dtos.Product;

namespace ECommerceManager.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7009/api/products";

        public ProductManager(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            return await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<ProductDto> CreateAsync(ProductDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, dto);
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<ProductDto> UpdateAsync(int id, ProductDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", dto);
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
