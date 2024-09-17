using FoodHub.Data.Repository.IRepository;
using FoodHub.Model;
using FoodHub.Model.Dtos;
using FoodHub.Services.IServices;
using System.Net.WebSockets;

namespace FoodHub.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        //Adds a new booking
        public async Task AddBookingAsync(BookingDto bookingDto)
        {
            //Create a new booking entity from the BookingDto
            var booking = new Booking
            {
                BookingDate = bookingDto.BookingDate,
                BookingTime = bookingDto.BookingTime,
                NumberOfSeats = bookingDto.NumberOfSeats,
                Fk_UserId = bookingDto.UserId,
                Fk_TableId = bookingDto.TableID
            };

            //Call the repository to add the new booking 
            await _bookingRepository.AddBookingAsync(booking);
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (existingBooking == null)
            {
                throw new KeyNotFoundException($"Booking with ID {bookingId} was not found");
            }

            //Call repository to delete booking 
            await _bookingRepository.DeleteBookingAsync(bookingId);
        }

        //Retrieve all bookings 
        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            return bookings.Select(b => new BookingDto
            {
                BookingId = b.BookingId,
                BookingDate = b.BookingDate,
                BookingTime = b.BookingTime,
                NumberOfSeats = b.NumberOfSeats,
                UserId = b.Fk_UserId,
                TableID = b.Fk_TableId
            }).ToList();
        }

        //Retrieves all available tables on a specific time
        public async Task<IEnumerable<TableDto>> GetAvailableTablesAsync(DateOnly date, TimeOnly time)
        {
            var availableTables = await _bookingRepository.GetAvaliableTablesAsync(date, time);
            return availableTables.Select(t => new TableDto
            {
                TableId = t.TableId,
                TableSeats = t.TableSeats,
                TableNumber = t.TableNumber,
                IsAwailable = t.IsAwailable,
            }).ToList();
        }

        //retrieves a specific booking by its ID
        public async Task<BookingDto> GetBookingByIdAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                return null;
            }

            return new BookingDto
            {
                BookingId = booking.BookingId,
                BookingDate = booking.BookingDate,
                BookingTime = booking.BookingTime,
                NumberOfSeats = booking.NumberOfSeats,
                UserId = booking.Fk_UserId,
                TableID = booking.Fk_TableId
            };
        }

        //Updates an exisiting booking
        public async Task UpdateBookingAsync(BookingDto bookingDto)
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(bookingDto.BookingId);
            if (existingBooking == null)
            {
                throw new KeyNotFoundException($"Booking with ID {bookingDto.BookingId} was not found");
            }

            //Update booking properties
            existingBooking.BookingDate = bookingDto.BookingDate;
            existingBooking.BookingTime = bookingDto.BookingTime;
            existingBooking.NumberOfSeats = bookingDto.NumberOfSeats;
            existingBooking.Fk_UserId = bookingDto.UserId;
            existingBooking.Fk_TableId = bookingDto.TableID;

            //save the changes to the repository 
            await _bookingRepository.UpdateBookingAsync(existingBooking);
        }
    }
}
