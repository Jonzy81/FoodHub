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

        //private string GenerateJwtToken(Admin admin)
        //{
        //    try
        //    {
        //        List<Claim> claims = new List<Claim>{
        //        new Claim(JwtRegisteredClaimNames.Email, admin.Email),
        //        new Claim(JwtRegisteredClaimNames.Name, $"{admin.FirstName} {admin.LastName}")
        //        // Optionally: new Claim(ClaimTypes.Role, "Admin")
        //    };
        //        // Generate the signing key and credentials
        //        SymmetricSecurityKey secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")));
        //        SigningCredentials credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        //        // Create JWT
        //        JwtSecurityToken jwt = new JwtSecurityToken(
        //            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
        //            audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
        //            claims: claims,
        //            expires: DateTime.Now.AddHours(1),
        //            signingCredentials: credentials
        //        );
        //        // Serialize and return the token
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        return tokenHandler.WriteToken(jwt);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Error generating JWT token", ex);
        //    }
        //}
    }
}
