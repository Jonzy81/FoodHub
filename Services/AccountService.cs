using FoodHub.Data.Repository.IRepository;
using FoodHub.Model;
using FoodHub.Model.Dtos;
using FoodHub.Services.IServices;

namespace FoodHub.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly JwtService _jwtService;
      
        public AccountService(IAccountRepository accountRepository, JwtService jwtService)
        {
            _accountRepository = accountRepository;
            _jwtService = jwtService;
            
        }
        public async Task<string> LoginAsync(LoginAdminDto loginAdmin)
        {
            var user = await _accountRepository.GetAdminByEmailAsync(loginAdmin.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginAdmin.Password, user.Passwordhash))
            {
                return null;
            }
            var token = await _jwtService.GenerateJwtTokenAsync(user);
            return token;
        }

        public async Task<(bool, string)> RegisterAsync(RegisterUserDto registerUser)
        {
            var existingUser = await _accountRepository.GetAdminByEmailAsync(registerUser.Email);
            if (existingUser != null)
            {
                return (false, "email is already in use");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);

            var newAccount = new Admin
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Email = registerUser.Email,
                Passwordhash = passwordHash,
            };

            await _accountRepository.AddAdminAsync(newAccount);
            return (true, null);
        }
    }
}
