using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RuneSharper.Services.Stats;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Settings;
using System.ComponentModel.DataAnnotations;

namespace RuneSharper.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountRequest request)
        {
            if (await UserExists(request.Email))
            {
                return BadRequest($"User with email {request.Email} already exists");
            }

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRequest request)
        {
            if (await UserExists(request.Email))
            {
                return BadRequest($"User with email {request.Email} already exists");
            }

            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            return Ok(result.Succeeded);
        }

        private Task<bool> UserExists(string email)
        {
            return _userManager.Users.AnyAsync(x => x.Email == email);
        }
    }

    public class AccountRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
