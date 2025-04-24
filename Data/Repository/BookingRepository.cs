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

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.Include(b => b.Table).Include(b => b.User).ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings.Include(b => b.Table).Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

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

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}
