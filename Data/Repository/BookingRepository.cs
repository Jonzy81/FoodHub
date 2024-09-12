using FoodHub.Data.Repository.IRepository;
using FoodHub.Model;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FoodHub.Data.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly RestaurantContext _context;
        public BookingRepository(RestaurantContext context)
        {
            _context = context;
        }

        //retrieves all bookings from the database
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.Include(b => b.Table).Include(b => b.User).ToListAsync();
        }

        //Retrieves a specific booking by its ID
        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings.Include(b => b.Table).Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        //Adds a new booking to the database
        public async Task AddBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        //Deletes a specific booking from the database
        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        //Retrieves all available tables on a specific date and time
        public async Task<IEnumerable<Table>> GetAvaliableTablesAsync(DateOnly date, TimeOnly time)
        {
            var bookedTables = await _context.Bookings
                .Where(b => b.BookingDate == date && b.BookingTime == time)
                .Select(b => b.Fk_TableId)
                .ToListAsync();

            return await _context.Tables
                .Where(t => !bookedTables.Contains(t.TableId))
                .ToListAsync();
        }    

        //Updates an existing booking in the database
        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}
