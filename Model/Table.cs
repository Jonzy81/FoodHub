using System.ComponentModel.DataAnnotations;

namespace FoodHub.Model
{
    public class Table
    {
        [Key] 
        public int TableId { get; set; }
        public string TableSeats { get; set; }
        public int TableNumber { get; set; }
        ICollection<Booking> Bookings { get; set; }

    }
}
