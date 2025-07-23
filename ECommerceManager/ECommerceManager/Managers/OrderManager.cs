using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Order;
using Newtonsoft.Json;

namespace ECommerceManager.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44321/api/orders";

        public OrderManager(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<List<OrderDetailsDto>> GetAllAsync()
        {
            var url = $"{_baseUrl}/GetAll";
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

        //public async Task<UpdateOrderDto> UpdateAsync(UpdateOrderDto dto)
        //{
        //    var url = $"{_baseUrl}/Update";
        //    var response = await _httpClient.PostAsJsonAsync(url, dto);
        //    var responseStr = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject < UpdateOrderDto > responseStr;
        //}

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
