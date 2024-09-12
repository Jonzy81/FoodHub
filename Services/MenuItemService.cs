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

        public async Task<MenuItemDto> GetMenuItemByIdAsync(int menuItemId)
        {
            var menuItem = await _menuItemrepository.GetMenuItemByIdAsync(menuItemId);
            if (menuItem == null)
            {
                return null;
            }

            return new MenuItemDto
            {
                MenuId = menuItem.MenuId,
                MenuType = menuItem.MenuType,
                MenuName = menuItem.MenuName,
                Description = menuItem.Description,
                Price = menuItem.Price,
                IsAwailable = menuItem.IsAwailable
            };

        }
        public async Task AddMenuItemAsync(MenuItemDto menuItemDto)
        {
            var menuItem = new MenuItem
            {
                MenuType = menuItemDto.MenuType,
                MenuName = menuItemDto.MenuName,
                Description = menuItemDto.Description,
                Price = menuItemDto.Price,
                IsAwailable = menuItemDto.IsAwailable
            };
            await _menuItemrepository.AddMenuItemAsync(menuItem);
        }

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
        public async Task DeleteMenuItem(int menuItemId)
        {
            await _menuItemrepository.DeleteMenuItemAsync(menuItemId);
        }
    }
}
