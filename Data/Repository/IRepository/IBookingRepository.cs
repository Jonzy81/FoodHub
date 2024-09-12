using FoodHub.Model;

namespace FoodHub.Data.Repository.IRepository
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int bookingId);
        Task<IEnumerable<Table>> GetAvaliableTablesAsync(DateOnly date, TimeOnly time);
    }
}
