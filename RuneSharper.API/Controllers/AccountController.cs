using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RuneSharper.Shared.Entities;
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
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not null)
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
    }

    public class AccountRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
