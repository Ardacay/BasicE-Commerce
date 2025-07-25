using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Order;
using ECommerceManager.Dtos.Product;
using Newtonsoft.Json;

namespace ECommerceManager.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44321/api/Order";

        public OrderManager(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }


        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var url = $"{_baseUrl}/GetAll";
            var response = await _httpClient.GetAsync(url);
            var contentStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(contentStr);
        }


        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var url = $"{_baseUrl}/GetById/{id}";
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadFromJsonAsync<OrderDto>();
        }

        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            var ulr = $"{_baseUrl}/Create";
            var response = await _httpClient.PostAsJsonAsync(ulr, dto);
            return await response.Content.ReadFromJsonAsync<OrderDto>();
        }

        public async Task<OrderDto> UpdateAsync(OrderDto dto)
        {
            var url = $"{_baseUrl}/Update";
            var response = await _httpClient.PostAsJsonAsync(url, dto);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API error: {response.StatusCode}, {errorContent}");
            }
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderDto>(responseString);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var url = $"{_baseUrl}/Delete/{id}";
            var response = await _httpClient.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
            }

            return response.IsSuccessStatusCode;
        }


    }
}
