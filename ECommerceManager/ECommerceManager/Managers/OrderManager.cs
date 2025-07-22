using ECommerceManager.Dtos.Order;

namespace ECommerceManager.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44321/api/orders";

        public OrderManager(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<List<OrderDetailsDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            return await response.Content.ReadFromJsonAsync<List<OrderDetailsDto>>();
        }

        public async Task<OrderDetailsDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            return await response.Content.ReadFromJsonAsync<OrderDetailsDto>();
        }

        public async Task<OrderDetailsDto> CreateAsync(OrderCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, dto);
            return await response.Content.ReadFromJsonAsync<OrderDetailsDto>();
        }
    }
}
