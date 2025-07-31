using ECommerceManager.Dtos.RoleDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ECommerceManager.Controllers
{
    public class RoleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl = "https://localhost:44336/api/Role";
        public RoleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {
            var url = $"{_baseUrl}/GetAllUsersWithRoles/GetAllUsersWithRoles";
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return View("Error");
            var json = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserRoleDto>>(json);
            return View(users);
        }

        public async Task<IActionResult> GiveRole()
        {
            List<UserRoleDto> users = new();

            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync("https://localhost:44336/api/User/GetAllUsersWithRoles");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<UserRoleDto>>(json);
                }

            }
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync("https://localhost:44336/api/Role/GetAllRoles");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var roles = JsonConvert.DeserializeObject<List<UserRoleDto>>(json);
                    ViewBag.AllRoles = roles.Select(r => r.Roles).ToList();
                }
            }
            return View(users);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateDto dto)
        {
            var url = $"{_baseUrl}/UpdateUserRole";
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                UserId = dto.Id,
                NewRole = dto.NewRole,
            }));
            await client.PostAsync(url, content);
            return RedirectToAction("Index");
        }
    }
}
