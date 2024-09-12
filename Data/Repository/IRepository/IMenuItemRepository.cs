﻿using FoodHub.Model;

namespace FoodHub.Data.Repository.IRepository
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem> GetMenuItemByIdAsync(int menuItemId);
        Task AddMenuItemAsync(MenuItem menuItem);
        Task UpdateMenuItem(MenuItem menuItem);
        Task DeleteMenuItemAsync(int menuItemId);
    }
}
