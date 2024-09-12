using FoodHub.Data.Repository.IRepository;
using FoodHub.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodHub.Data.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly RestaurantContext _context;

        public MenuItemRepository(RestaurantContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _context.MenuItems.ToListAsync();
        }
        public async Task<MenuItem> GetMenuItemByIdAsync(int menuItemId)
        {
            return await _context.MenuItems.FindAsync(menuItemId);
        }
        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuItemAsync(int menuItemId)
        {
            var menuItem = await _context.MenuItems.FindAsync(menuItemId);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}
