using FoodHub.Data;
using FoodHub.Model;
using FoodHub.Model.Dtos;
using FoodHub.Services;
using FoodHub.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;        

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
            
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUser)
        {
            var (isRegistered, errorMessage) = await _accountService.RegisterAsync(registerUser);
            if ( isRegistered)
            {
                return Ok();
            }
            return BadRequest(errorMessage);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginAdminDto loginAdmin)
        {
            var token = await _accountService.LoginAsync(loginAdmin);
            if (token == null)
            {
                return Unauthorized("invalid email or password");
            }
            return Ok(new {token });
        }

    }
}
