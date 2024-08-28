using System.ComponentModel.DataAnnotations;

namespace FoodHub.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(255)]
        public string UserEmail { get; set; }
        [Required]
        [MaxLength(25)]
        public string UserPhoneNumber { get; set; }       

        ICollection<Booking> bookings { get; set; }
    }
}
