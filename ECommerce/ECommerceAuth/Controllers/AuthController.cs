using ECommerceAuth.Dtos;
using ECommerceAuth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthController(UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

    

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                FirstName=model.Name,
                LastName=model.LastName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _userManager.AddToRoleAsync(user, "Member");
            return Ok("Register Success");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized("Invalid email or password");
            }
            await _signInManager.SignInAsync(user, false);
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("email", user.Email ?? ""),
                new Claim("name", user.FirstName ?? ""),
                new Claim("lastname", user.LastName ?? ""),
                new Claim("phone", user.PhoneNumber ?? ""),
                new Claim("birthdate", user.BirthDate.HasValue ? user.BirthDate.Value.ToString("yyyy-MM-dd") : "")

            };
            foreach (var role in userRoles)
            authClaims.Add(new Claim(ClaimTypes.Role, role));
            var authoSighnKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.UtcNow.AddHours(4),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authoSighnKey, SecurityAlgorithms.HmacSha256)
            );
            var writedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new
            {
                token = writedToken
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new
            {
                user.Email,
                user.UserName,
                user.BirthDate,
                user.FirstName, 
                user.LastName,
                user.PhoneNumber,
                roles = roles
            });
        }
    }



}





