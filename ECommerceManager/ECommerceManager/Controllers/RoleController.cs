using ECommerceManager.Dtos.RoleDtos;
using ECommerceManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private readonly HttpClient _client;

        public RoleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient();
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            //var url = $"{_baseUrl}/GetAllUsersWithRoles/GetAllUsersWithRoles";
            ////var client = _httpClientFactory.CreateClient();
            //var token = Request.Cookies["access_token"];
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var response = await _client.GetAsync(url);
            //if (!response.IsSuccessStatusCode)
            //{
            //    var errorContent = await response.Content.ReadAsStringAsync();
            //    return View(errorContent);
            //}
            //var json = await response.Content.ReadAsStringAsync();
            //var users = JsonConvert.DeserializeObject<List<UserRoleDto>>(json);
            //var urlRole = $"{_baseUrl}/GetAllRoles/GetAllRoles";
            //var responseRole = await _client.GetAsync(urlRole);

            //if (responseRole.IsSuccessStatusCode)
            //{
            //    var jsonRole = await responseRole.Content.ReadAsStringAsync();
            //    var roles = JsonConvert.DeserializeObject<List<RoleUpdateDto>>(jsonRole);
            //    ViewBag.AllRoles = roles.Select(r => r.Name).ToList();
            //}
            //return View(users);
            var userWithAllRoles = await GetUserWithAllRoles();
            return View(userWithAllRoles);
        }

        private async Task<UserWithAllRolesDto> GetUserWithAllRoles()
        {
            var token = Request.Cookies["access_token"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var userUrl = $"{_baseUrl}/GetAllUsersWithRoles/GetAllUsersWithRoles";
            var userResponse = await _client.GetAsync(userUrl);

            if (!userResponse.IsSuccessStatusCode)
                return new UserWithAllRolesDto();

            var userJson = await userResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserRoleDto>>(userJson);

            var roleUrl = $"{_baseUrl}/GetAllRoles/GetAllRoles";
            var roleResponse = await _client.GetAsync(roleUrl);
            var allRoles = new List<RoleDto>();

            if (roleResponse.IsSuccessStatusCode)
            {
                var roleJson = await roleResponse.Content.ReadAsStringAsync();
                allRoles = JsonConvert.DeserializeObject<List<RoleDto>>(roleJson);
            }

            return new UserWithAllRolesDto
            {
                Users = users,
                AllRoles = allRoles,
                RoleUpdateDto = new RoleUpdateDto()
            };
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateDto dto)
        {
            var url = $"{_baseUrl}/UpdateUserRole";
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(new RoleUpdateDto
            {
                UserId = dto.UserId,
                RoleId = dto.RoleId,
                //Name = dto.Name,
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {

                return View("Error");
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRoleAjax([FromBody] RoleUpdateDto dto)
        {
            var url = $"{_baseUrl}/UpdateUserRole";
            //var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["access_token"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (dto.UserId.IsNullOrEmpty() || dto.RoleId.IsNullOrEmpty())
                return Json(new { IsSucceeded = false, Message = "Id veya Rol İsmi zorunlu alandır!" });
            var content = new StringContent(JsonConvert.SerializeObject(new RoleUpdateDto
            {
                UserId = dto.UserId,
                RoleId = dto.RoleId,
                //Name = dto.Name,
            }), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { IsSucceeded = false, Message = "Sunucu hatası!" });
            }
            var updateViewModel = await GetUserWithAllRoles();

            return PartialView("_UpdateRolePartialView", updateViewModel);
        }

    }
}
