using ECommerceManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
namespace ECommerceManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel logmodel)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(logmodel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44336/api/Account/Login", content); /*5001*/
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Access Denied";
                return View();
            }
            var jsonData = await response.Content.ReadAsStringAsync();
            dynamic tokendata = JsonConvert.DeserializeObject(jsonData)!;

            string token = tokendata.ToString();
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var claims = jwtToken.Claims.ToList();
            claims.Add(new Claim("AccessToken", token));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel regmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(regmodel);
            }
            var client=_httpClientFactory.CreateClient();

            var json=JsonConvert.SerializeObject(regmodel);
            var content=new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44336/api/Account/Register", content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Kayıt başarısız oldu.";
                return View(regmodel);
            }

          
            return RedirectToAction("Login", "Account");

        }
    }
}

