using ECommerceManager.Dtos.Product;

namespace ECommerceManager.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44321/api/products";

        public ProductManager(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var url = $"{_baseUrl}/GetAll";
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var url = $"{_baseUrl}/GetById/{id}";
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<ProductDto> CreateAsync(ProductDto dto)
        {
            var url = $"{_baseUrl}/Create";
            var response = await _httpClient.PostAsJsonAsync(url, dto);
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<ProductDto> UpdateAsync(int id, ProductDto dto)
        {
            var url = $"{_baseUrl}/Update/{id}";
            var response = await _httpClient.PutAsJsonAsync(url, dto);
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var url = $"{_baseUrl}/Delete/{id}";
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
