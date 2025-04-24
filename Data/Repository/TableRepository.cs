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

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _context.Tables.ToListAsync();
        }

        public async Task<Table> GettableIdAsync(int tableId)
        {
            return await _context.Tables.FindAsync(tableId);
        }

        public async Task AddtableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTableAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeletetableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, DateOnly date, TimeOnly time)
        {
            return !await _context.Bookings.AnyAsync(b => 
                b.Fk_TableId == tableId &&  
                b.BookingDate == date &&    
                b.BookingTime == time       
            );
        }

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
