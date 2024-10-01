using FoodHub.Model.Dtos;

namespace FoodHub.Services.IServices
{
    public interface IAccountService
    {
        Task<(bool, string)> RegisterAsync(RegisterUserDto registerUser);
        Task<string> LoginAsync(LoginAdminDto loginAdmin);
    }
}
