using FoodHub.Model.Dtos;

namespace FoodHub.Services.IServices
{
    public interface ITableService
    {
        Task<IEnumerable<TableDto>> GetAllTablesAsync();
        Task<TableDto> GetTablebyIdAsync(int tableId);
        Task AddTableAsync(TableDto table);
        Task UpdatetableAsync(TableDto tableDto);
        Task DeleteTableAsync(int tableId);
        Task<bool> IsTableAvailableAsync(int tableId, DateOnly date, TimeOnly time);
        Task<IEnumerable<TableDto>> GetAvailableTablesAsync(DateOnly date, TimeOnly time);
    }
}
