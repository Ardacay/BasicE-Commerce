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
            var url = $"{_baseUrl}/GetAll";
            var response = await _httpClient.GetAsync(url);
            var contentStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CategoryDto>>(contentStr);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            //var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            //var contentStr = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<CategoryDto>(contentStr);

            var url = $"{_baseUrl}/GetById/{id}";
            var response = await _httpClient.GetAsync(url);
            var contentStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CategoryDto>(contentStr);

        }

        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            var url = $"{_baseUrl}/CreateCategoryies";
            var response = await _httpClient.PostAsJsonAsync(url, dto);
            var contentStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CategoryDto>(contentStr);


        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryDto dto)
        {
            var url = $"{_baseUrl}/Update/{id}";
            var jsonBody = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", content);
            var response = await _httpClient.PutAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CategoryDto>(responseString);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var url = $"{_baseUrl}/Delete/{id}";
            var response = await _httpClient.DeleteAsync(url);
            //var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
