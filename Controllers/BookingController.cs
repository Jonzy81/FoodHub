using FoodHub.Model.Dtos;
using FoodHub.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/Booking
        // Retrieves all bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // GET: api/Booking/{id}
        // Retrieves a specific booking by its ID
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // POST: api/Booking
        // Adds a new booking
        [HttpPost]
        public async Task<ActionResult> AddBooking([FromBody] BookingDto bookingDto)
        {
            if (bookingDto == null)
            {
                return BadRequest();
            }

            await _bookingService.AddBookingAsync(bookingDto);
            return CreatedAtAction(nameof(GetBookingById), new { id = bookingDto.BookingId }, bookingDto);
        }

        // PUT: api/Booking/{id}
        // Updates an existing booking
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBooking(int id, [FromBody] BookingDto bookingDto)
        {
            if (bookingDto == null || bookingDto.BookingId != id)
            {
                return BadRequest();
            }

            await _bookingService.UpdateBookingAsync(bookingDto);
            return NoContent();
        }

        // DELETE: api/Booking/{id}
        // Deletes a specific booking by its ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return NoContent();
        }

    }
}
