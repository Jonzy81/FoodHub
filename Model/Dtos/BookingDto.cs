namespace FoodHub.Model.Dtos
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public int NumberOfSeats { get; set; }
        public int UserId { get; set; }    
        public int TableID { get; set; }  
    }
}
