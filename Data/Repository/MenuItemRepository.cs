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
        //Retrieves a list of all menu items in the database
        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _context.MenuItems.ToListAsync();
        }

        //retrieves a specific menu item by its ID 
        public async Task<MenuItem> GetMenuItemByIdAsync(int menuItemId)
        {
            return await _context.MenuItems.FindAsync(menuItemId); //The corresponding menuItem entity or null if not found
        }
        //Adds a new menu item to the database
        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }
        //deletes a specific menu item from the database by its ID 
        public async Task DeleteMenuItemAsync(int menuItemId)
        {
            var menuItem = await _context.MenuItems.FindAsync(menuItemId);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }
        //Updates an existing menu item to the database
        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}
