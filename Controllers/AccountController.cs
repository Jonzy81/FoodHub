using FoodHub.Data;
using FoodHub.Model;
using FoodHub.Model.Dtos;
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
        private readonly RestaurantContext _context;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly string _jwtSecret;
        public AccountController(RestaurantContext context)
        {
            _context = context;

            _jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER is not configured.");
            _jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new InvalidOperationException("JWT_AUDIENCE is not configured.");
            _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new InvalidOperationException("JWT_SECRET is not configured.");
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterUserDto registerUser)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.UserEmail == registerUser.Email);

            if (existingUser != null)
            {
                return BadRequest("Email is already in use");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);

            var newAccount = new Admin
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Email = registerUser.Email,
                Passwordhash = passwordHash
            };

            _context.Admins.Add(newAccount);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginAdminDto loginAdmin)
        {
            var user = _context.Admins.SingleOrDefault(u => u.Email == loginAdmin.Email);   //Check if Email and password already exists amongs admins

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginAdmin.Password, user.Passwordhash))
            {
                return Unauthorized("Invald email or password");
            }
            var token = GenerateJwtToken(user);
            return Ok(new {token});
        }

        private string GenerateJwtToken(Admin admin)
        {
            // Validate that the required JWT configuration values are present
            if (string.IsNullOrEmpty(_jwtSecret) || string.IsNullOrEmpty(_jwtIssuer) || string.IsNullOrEmpty(_jwtAudience))
            {
                throw new InvalidOperationException("JWT configuration is not set properly.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);
            var issuer = _jwtIssuer;
            var audience = _jwtAudience;

            //Set claims for JWT
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                //new Claim(ClaimTypes.Role, "Admin"),    //Hårdkodad roll 
                new Claim(ClaimTypes.Email, admin.Email)
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
