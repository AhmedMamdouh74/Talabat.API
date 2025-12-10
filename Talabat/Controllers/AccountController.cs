using Application.DTOs.Account;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, ILogger<AccountController> logger)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            this.logger = logger;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Error("Unauthrized", StatusCodes.Status401Unauthorized);
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Error("Unauthrized", StatusCodes.Status401Unauthorized);
            var userDto = new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "This is a token"
            };
            return Success(userDto);
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var existingUser = await userManager.FindByEmailAsync(registerDto.Email);


            if (existingUser != null)
            {
                logger.LogWarning("Registration attempt with existing email: {Email}", registerDto.Email);
                return Error("Invalid registration details", StatusCodes.Status400BadRequest);
            }
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split('@')[0]
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) Error("Problem registering user", StatusCodes.Status400BadRequest);

            return Success(new UserDto() { DisplayName = registerDto.DisplayName, Email = registerDto.Email, Token = "this is token" });

        }
    }
}
