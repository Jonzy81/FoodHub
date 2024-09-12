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
        //Adds user to the repository 
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        //deletes user from repository by id 
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null) 
            {
                _context.Users.Remove(user);
            }
            await _context.SaveChangesAsync();

        }
        //Retrieves a list of users from repository 
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var userList = await _context.Users.ToListAsync();
            return userList;
        }
        //retrieves user from repository by id 
        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            return user; 
        }
        //Updates user 
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

        }
    }
}
