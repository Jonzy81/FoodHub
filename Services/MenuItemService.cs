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

        public Task AddMenuItemAsync(MenuItemDto menuItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMenuItem(int menuItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MenuItemDto> GetMenuItemByIdAsync(int menuItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMenuItemAsync(MenuItemDto menuItem)
        {
            throw new NotImplementedException();
        }
    }
}
