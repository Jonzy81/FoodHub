using FoodHub.Model;
using FoodHub.Model.Dtos;

namespace FoodHub.Services.IServices
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemDto>> GetAllMenuItemsAsync();
        Task<MenuItemDto>GetMenuItemByIdAsync(int menuItemId);
        Task AddMenuItemAsync(MenuItemDto menuItem);
        Task UpdateMenuItemAsync(MenuItemDto menuItem);
        Task DeleteMenuItemAsync(int menuItemId);
    }
}
