using ECommerceManager.Dtos.Categories;
using ECommerceManager.İnterfaces;

namespace ECommerceManager.Managers
{
    public class GenericManager<TDto> : IManager<TDto>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44321/api/categories";

        public GenericManager(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl.TrimEnd('/');
        }


       

        public async Task<List<TDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TDto>>(_baseUrl);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TDto>($"{_baseUrl}/{id}");
        }

        public async Task<bool> CreateAsync(TDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(TDto dto)
        {
            var idProp = typeof(TDto).GetProperty("Id");
            if (idProp == null) throw new Exception("TDto must have an Id property");

            var id = idProp.GetValue(dto);
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
