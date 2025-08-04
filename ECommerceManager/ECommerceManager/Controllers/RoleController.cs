using ECommerceManager.Dtos.RoleDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
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

        [Authorize]
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
            var urlRole = $"{_baseUrl}/GetAllRoles/GetAllRoles";
            var responseRole = await client.GetAsync(urlRole);

            if (responseRole.IsSuccessStatusCode)
            {
                var jsonRole = await responseRole.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<RoleUpdateDto>>(jsonRole);
                ViewBag.AllRoles = roles.Select(r => r.Name).ToList();
            }
            return View(users);
        }

        //public async Task<IActionResult> GiveRole()
        //{
        //    List<UserRoleDto> users = new();

        //    using (var client = _httpClientFactory.CreateClient())
        //    {
        //        var response = await client.GetAsync("https://localhost:44336/api/User/GetAllUsersWithRoles");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var json = await response.Content.ReadAsStringAsync();
        //            users = JsonConvert.DeserializeObject<List<UserRoleDto>>(json);
        //        }

        //    }
        //    using (var client = _httpClientFactory.CreateClient())
        //    {
        //        var response = await client.GetAsync("https://localhost:44336/api/Role/GetAllRoles");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var json = await response.Content.ReadAsStringAsync();
        //            var roles = JsonConvert.DeserializeObject<List<UserRoleDto>>(json);
        //            ViewBag.AllRoles = roles.Select(r => r.Roles).ToList();
        //        }
        //    }
        //    return View(users);
        //}


        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateDto dto)
        {
            var url = $"{_baseUrl}/UpdateUserRole";
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(new RoleUpdateDto
            {
                Id = dto.Id,
                Name = dto.Name,
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {

                return View("Error");
            }

            return RedirectToAction("Index");
        }

    }
}
