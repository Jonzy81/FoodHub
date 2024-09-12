﻿using FoodHub.Model;

namespace FoodHub.Data.Repository.IRepository
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table>GettableIdAsync(int tableId);
        Task AddtableAsync(Table table);
        Task UpdateTableAsync(Table table);
        Task DeletetableAsync(int tableId);
        Task<bool> IsTableAvailable(int tableId, DateOnly date, TimeOnly time);
    }
}
