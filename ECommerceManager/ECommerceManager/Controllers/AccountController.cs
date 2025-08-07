using ECommerceManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ECommerceManager.Dtos.AccountDtos;
using ECommerceManager.Dtos.TokenResult;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
namespace ECommerceManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            //_signInManager = signInManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var token = Request.Cookies["access_token"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token alınamadı");
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("https://localhost:44336/api/Auth/GetProfile");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var json = await response.Content.ReadAsStringAsync();
            var profile = JsonConvert.DeserializeObject<ProfileDto>(json);
            return View(profile);
        }


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto logmodel, string returnUrl=null)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(logmodel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44336/api/Auth/Login/Login", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return View(errorContent);
            }
            var jsonData = await response.Content.ReadAsStringAsync();
            var tokendata = JsonConvert.DeserializeObject<TokenResult>(jsonData)!;


            string username = HttpContext.Request.Cookies["access_token"];

            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokendata.Token);

            var claims = jwtToken.Claims.ToList();
            claims.Add(new Claim("AccessToken", tokendata.Token));

            Response.Cookies.Append("access_token", tokendata.Token, new CookieOptions
            {
                HttpOnly = true,
                Expires = jwtToken.ValidTo
            });

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
              if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            return RedirectToAction("Profile", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("access_token");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto regmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(regmodel);
            }
            var client = _httpClientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(regmodel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44336/api/Auth/Register/Register", content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Kayıt başarısız oldu.";
                return View(regmodel);
            }


            return RedirectToAction("Login", "Account");

        }
    }
}

