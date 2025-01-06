using DAL.Entities;
using Exam.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Register Action
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "User");
                return Ok(new { Message = "تم انشاء حساب بنجاح" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return BadRequest(ModelState);
        }

        // Login Action
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    Success = result.Succeeded,
                    Message = "تم تسجيل الدخول",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    IsAdmin = await IsAdmin()
                });
            }

            return Unauthorized(new
            {
                Success = result.Succeeded,
                Message = "اسم مستخدم او كلمه مرور خاطئه"
            });
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var userList = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "لا يوجد صلاحيه"
                });
            }

            return Ok(userList);

        }


        // Logout Action
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "تم تسجيل الخروج" });
        }


        [Authorize]
        [HttpGet("GetUaserId")]
        public async Task<IActionResult> GetUaserId() => Ok(new { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) });
        [Authorize]
        [HttpGet("IsAdmin")]
        public async Task<bool> IsAdmin()
        {

            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            return isAdmin;
        }

        // Logout Action
        [Authorize]
        [HttpPut("EditUser")]
        public async Task<IActionResult> AddAdminRole([FromBody] UserDto _user)
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);
            }
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                await _roleManager.CreateAsync(role);
            }
            var user = await _userManager.FindByIdAsync(_user.Id.ToString());

            if (user == null)
            {
                return NotFound(new { message = "المستخدم غير موجود" });
            }

            // أضف المستخدم إلى دور "Admin"
            if (await _userManager.IsInRoleAsync(user, "Admin"))
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            if (await _userManager.IsInRoleAsync(user, "User"))
                await _userManager.RemoveFromRoleAsync(user, "User");

            var result = await _userManager.AddToRoleAsync(user, _user.Role);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "حدث خطاء اثناء حفظ التغييرات", errors = result.Errors });
            }

            var Result = await _userManager.UpdateAsync(user);
            if (!Result.Succeeded)
            {
                return BadRequest(new { message = "حدث خطاء اثناء حفظ التغييرات", errors = result.Errors });
            }

            return Ok(new { message = "تم حفظ التغيرات" });
        }


        // Change Password Action
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized(new { Message = "User not found" });

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Password changed successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return BadRequest(ModelState);
        }
    }
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
