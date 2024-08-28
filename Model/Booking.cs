using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHub.Model
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public DateOnly BookingDate { get; set; }
        [Required]
        public TimeOnly BookingTime { get; set; }
        [Required]
        public int NumberOfSeats { get; set; }
        [ForeignKey("User")]
        public int Fk_UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Table")]
        public int Fk_TableId { get; set; }
        public Table Table { get; set; }
    }
}
