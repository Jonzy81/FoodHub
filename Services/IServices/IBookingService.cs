using FoodHub.Model.Dtos;

namespace FoodHub.Services.IServices
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> GetBookingByIdAsync(int bookingId);
        Task AddBookingAsync(BookingDto bookingDto);
        Task UpdateBookingAsync(BookingDto bookingDto);
        Task DeleteBookingAsync(int bookingId);
        Task<IEnumerable<TableDto>> GetAvailableTablesAsync(DateOnly date, TimeOnly time);
    }
}
