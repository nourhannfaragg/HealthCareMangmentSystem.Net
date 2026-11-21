using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.NET.Dtos;
using WebApi.NET.Models;
using WebApi.NET.Results;
using WebApi.NET.Services;

namespace WebApi.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager ,IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Rigester(UserRegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email=request.Email
            };
            var result = await _userManager.CreateAsync(user,request.password);

            if(!result.Succeeded) 
                return BadRequest(result.Errors);

            return Created();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> login(UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user is null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.password, false);

            if (!result.Succeeded)
                return Unauthorized();

            var token = _jwtService.GenerateToken(user);

            return Ok(GenericResult<string>.Success(token));
        }
    }
}
