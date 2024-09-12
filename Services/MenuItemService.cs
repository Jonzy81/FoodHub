using FoodHub.Data.Repository.IRepository;
using FoodHub.Model;
using FoodHub.Model.Dtos;
using FoodHub.Services.IServices;

namespace FoodHub.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemrepository;
        public MenuItemService(IMenuItemRepository menuItemrepository)
        {
            _menuItemrepository = menuItemrepository;
        }

        //Retrieves all menu items from the repository 
        //And returns a list of MenuItemDto representing all menu items 
        public async Task<IEnumerable<MenuItemDto>> GetAllMenuItemsAsync()
        {
            var menuItems = await _menuItemrepository.GetAllMenuItemsAsync();
            return menuItems.Select(x => new MenuItemDto
            {
                MenuId = x.MenuId,
                MenuType = x.MenuType,
                MenuName = x.MenuName,
                Description = x.Description,
                Price = x.Price,
                IsAwailable = x.IsAwailable
            }).ToList();
        }

        //Retrieves a specific menu item by its Id 
        public async Task<MenuItemDto> GetMenuItemByIdAsync(int menuItemId) //menuItemId, the name of the menu item to retrieve
        {
            var menuItem = await _menuItemrepository.GetMenuItemByIdAsync(menuItemId);
            if (menuItem == null)
            {
                return null;    //Returns null if not found
            }

            return new MenuItemDto  //A menuItemDto representing the menu item 
            {
                MenuId = menuItem.MenuId,
                MenuType = menuItem.MenuType,
                MenuName = menuItem.MenuName,
                Description = menuItem.Description,
                Price = menuItem.Price,
                IsAwailable = menuItem.IsAwailable
            };

        }
        //Adds a new menu item to the repository
        public async Task AddMenuItemAsync(MenuItemDto menuItemDto)
        {
            var menuItem = new MenuItem //The menu item data to add
            {
                MenuType = menuItemDto.MenuType,
                MenuName = menuItemDto.MenuName,
                Description = menuItemDto.Description,
                Price = menuItemDto.Price,
                IsAwailable = menuItemDto.IsAwailable
            };
            await _menuItemrepository.AddMenuItemAsync(menuItem);
        }
        //Updates an existing menu item in the repository 
        //the menu item data to update, including the ID 
        public async Task UpdateMenuItemAsync(MenuItemDto menuItemDto)
        {
            var menuItem = new MenuItem
            {
                MenuId = menuItemDto.MenuId,
                MenuType = menuItemDto.MenuType,
                MenuName = menuItemDto.MenuName,
                Description = menuItemDto.Description,
                Price = menuItemDto.Price,
                IsAwailable = menuItemDto.IsAwailable
            };

            await _menuItemrepository.UpdateMenuItemAsync(menuItem);
        }
        //Deletes a specific menu item from the repository by its Id
        public async Task DeleteMenuItemAsync(int menuItemId)
        {
            await _menuItemrepository.DeleteMenuItemAsync(menuItemId);
        }
    }
}
