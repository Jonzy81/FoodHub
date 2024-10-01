using FoodHub.Data.Repository.IRepository;
using FoodHub.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodHub.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly RestaurantContext _context;
        public AccountRepository(RestaurantContext context)
        {
            _context = context;
        }
        public async Task AddAdminAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            return await _context.Admins.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
