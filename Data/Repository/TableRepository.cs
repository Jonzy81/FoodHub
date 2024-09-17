using FoodHub.Data.Repository.IRepository;
using FoodHub.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodHub.Data.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly RestaurantContext _context;
        public TableRepository(RestaurantContext context)
        {
            _context = context;
        }

        //Retrieve all tables rom the database
        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _context.Tables.ToListAsync();
        }

        //Retrieves a specific table by its ID
        public async Task<Table> GettableIdAsync(int tableId)
        {
            return await _context.Tables.FindAsync(tableId);
        }

        //Adds a new table to the database
        public async Task AddtableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        //Updates an existing table in the database
        public async Task UpdateTableAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }

        //Deletes a specific table from the database
        public async Task DeletetableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
        }

        //Checks if a specific table is available on a given date or time
        //And correctly returns true if table is awailable and false if its booked 
        public async Task<bool> IsTableAvailableAsync(int tableId, DateOnly date, TimeOnly time)
        {
            return !await _context.Bookings.AnyAsync(b =>       //Use of ! before await to invert true operations true when booking exists and false when it doesnt 
                b.Fk_TableId == tableId &&  //check if any booking exists for the same table 
                b.BookingDate == date &&    //on the same date
                b.BookingTime == time       //on the same time
            );
        }


    }
}
