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

        // Retrieves all available tables on a specific date and time
        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(DateOnly date, TimeOnly time, int numberOfSeats)
        {
            var bookedTables = await _context.Bookings
                .Where(b => b.BookingDate == date && b.BookingTime == time)
                .Select(b => b.Fk_TableId)
                .ToListAsync();

            return await _context.Tables
                .Where(t => !bookedTables.Contains(t.TableId)
                    && (t.TableSeats) >= numberOfSeats
                    && (t.TableSeats) <= numberOfSeats + 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<DateOnly>> GetAvailableDatesAsync(int numberOfSeats)
        {
            var dates = await _context.Tables
                .Where(t => Convert.ToInt32(t.TableSeats) >= numberOfSeats &&
                            Convert.ToInt32(t.TableSeats) <= numberOfSeats + 1 &&
                            t.IsAwailable)
                .SelectMany(t => _context.Bookings
                    .Where(b => b.Fk_TableId == t.TableId)
                    .Select(b => b.BookingDate))
                .Distinct()
                .ToListAsync();

            return dates;
        }
        public async Task<IEnumerable<TimeOnly>> GetAvailableTimesAsync(DateOnly date, int numberOfSeats)
        {
            var allTimes = new List<TimeOnly>
            {
                new(17, 0), new(17, 30), new(18, 0), new(18, 30),
                new(19, 0), new(19, 30), new(20, 0), new(20, 30), new(21, 0)
            };

            var availableTimes = new List<TimeOnly>();

            foreach (var time in allTimes)
            {
                var bookedTableIds = await _context.Bookings
                .Where(b => b.BookingDate == date && b.BookingTime == time)
                .Select(b => b.Fk_TableId)
                .ToListAsync();

                var availableTables = await _context.Tables
                    .Where(t => !bookedTableIds.Contains(t.TableId)
                        && t.TableSeats >= numberOfSeats
                        && t.TableSeats <= numberOfSeats + 1)
                    .ToListAsync();

                if (availableTables.Any())
                {
                    availableTimes.Add(time);
                }
            }

            return availableTimes;
        }

    }
}
