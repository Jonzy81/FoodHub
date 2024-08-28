using FoodHub.Data.Repository.IRepository;
using FoodHub.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace FoodHub.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RestaurantContext _context;
        public UserRepository(RestaurantContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Customer.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Customer.FindAsync(userId);
            if (user != null) 
            {
                _context.Customer.Remove(user);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var userList = await _context.Customer.ToListAsync();
            return userList;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _context.Customer.FindAsync(userId);

            return user; 
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Customer.Update(user);
            await _context.SaveChangesAsync();

        }
    }
}
