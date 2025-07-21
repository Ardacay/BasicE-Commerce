using ECommerceManager.Dtos.Categories;
using Newtonsoft.Json;
using System.Text;

namespace ECommerceManager.Managers
{
    public class CategoryManager : ICategoryManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44321/api/Category";

        public CategoryManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            var contentStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CategoryDto>>(contentStr);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            var contentStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CategoryDto>(contentStr);
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, dto);
            var contentStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CategoryDto>(contentStr);
        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryDto dto)
        {
          
            var jsonBody = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CategoryDto>(responseString);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
