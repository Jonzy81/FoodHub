using FoodHub.Data.Repository;
using FoodHub.Model;

namespace FoodHub.Services
{
    public class JwtService
    {
        private readonly JwtRepository _jwtRepository;
        public JwtService(JwtRepository jwtRepository)
        {
            _jwtRepository = jwtRepository;
        }

        public async Task<string> GenerateJwtTokenAsync(Admin admin)
        {
            try
            {
                return await _jwtRepository.GenerateJwtTokenAsync(admin);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while generating JWT token", ex);
            }
        }
    }
}
