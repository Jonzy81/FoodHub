using FoodHub.Data.Repository.IRepository;
using FoodHub.Model;
using FoodHub.Model.Dtos;
using FoodHub.Services.IServices;




namespace FoodHub.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        //Adds a new table 
        public async Task AddTableAsync(TableDto tableDto)
        {
            var table = new Table
            {
                TableSeats = tableDto.TableSeats,
                TableNumber = tableDto.TableNumber, 
                IsAwailable = tableDto.IsAwailable,
            };
            await _tableRepository.AddtableAsync(table);
        }

        public Task DeleteTableAsync(int tableId)
        {
            throw new NotImplementedException();
        }

        //Retrieves all tables
        public async Task<IEnumerable<TableDto>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllTablesAsync();
            return tables.Select(t => new TableDto
            {
                TableId = t.TableId,
                TableSeats = t.TableSeats,
                TableNumber = t.TableNumber,
                IsAwailable = t.IsAwailable,
            }).ToList();
        }

        //Retrieve a specific table by its ID
        public async Task<TableDto> GetTablebyIdAsync(int tableId)
        {
            var table = await _tableRepository.GettableIdAsync(tableId);
            if (table == null)
            {
                return null;
            }
            return new TableDto
            {
                TableId = table.TableId,
                TableSeats = table.TableSeats,
                TableNumber = table.TableNumber,
                IsAwailable = table.IsAwailable
            };
        }

        public Task<bool> IsTableAvailableAsync(int tableId, DateOnly date, TimeOnly time)
        {
            throw new NotImplementedException();
        }

        public Task UpdatetableAsync(TableDto table)
        {
            throw new NotImplementedException();
        }
    }
}
