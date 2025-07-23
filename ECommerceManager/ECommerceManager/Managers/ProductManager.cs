using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Product;
using Newtonsoft.Json;

namespace ECommerceManager.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44321/api/Product";

        public ProductManager(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var url = $"{_baseUrl}/GetAll";
            var response = await _httpClient.GetAsync(url);
            var contentStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductDto>>(contentStr);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var url = $"{_baseUrl}/GetById/{id}";
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<ProductDto> CreateAsync(ProductCreateDto dto)
        {
            var url = $"{_baseUrl}/Create";
            var response = await _httpClient.PostAsJsonAsync(url, dto);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API error: {response.StatusCode}, {errorContent}");
            }

            var product = await response.Content.ReadFromJsonAsync<ProductDto>();
            return product;


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
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
