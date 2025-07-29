using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerceManager.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserManager(IHttpContextAccessor contextAccessor, IHttpClientFactory httpClientFactory)
        {
            _contextAccessor = contextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetUserEmailAsync()
        {
            var token = _contextAccessor.HttpContext.Request.Cookies["access_token"];

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:44336/api/Auth/Profile");
            if (!response.IsSuccessStatusCode)
                return "Giriş yapılmamış";

            var json = await response.Content.ReadAsStringAsync();
            dynamic user = JsonConvert.DeserializeObject(json);
            return user.email;
        }
    }
}
