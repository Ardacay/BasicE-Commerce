using ECommerceAuth.Dtos;
using ECommerceAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize/*(Roles = "Manager")*/]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("GetAllUsersWithRoles")]
        public async Task<IActionResult> GetAllUsersWithRoles()
        {
            var users = _userManager.Users.ToList();
            var result = new List<object>();


            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new
                {
                    user.Id,
                    user.UserName,
                    user.FullName,
                    Roles = roles.FirstOrDefault()
                });
            }
            return Ok(result);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.Select(role => new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            });
            return Ok(roles);
        }

        //[HttpPost]
        //[Authorize(Roles = "Member")]
        //public async Task<IActionResult> ChangeRole(string userId, string newRole)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //        return NotFound();

        //    var currentRoles = await _userManager.GetRolesAsync(user);

        //    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        //    if (!removeResult.Succeeded)
        //        return BadRequest("Roller kaldırılırken hata oluştu.");

        //    var addResult = await _userManager.AddToRoleAsync(user, newRole);
        //    if (!addResult.Succeeded)
        //        return BadRequest("Rol eklenirken hata oluştu.");

        //    return RedirectToAction("Index");
        //}
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(RoleUpdateDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.UserId) || string.IsNullOrEmpty(dto.RoleId))
                return BadRequest("Geçersiz rol bilgisi.");

            var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
                return BadRequest("Rolleri kaldıramıyoruz.");
            var role = await _roleManager.FindByIdAsync(dto.RoleId);
            if (role == null)
                return BadRequest("Rol bulunamadı.");

            var addResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!addResult.Succeeded)
                return BadRequest("Rol eklenmedi.");

            return Ok("Role Updated");
        }

    }
}
