using System.ComponentModel.DataAnnotations;

namespace FoodHub.Model
{
    public class MenuItem
    {
        [Key]
        public int MenuId { get; set; }
        public string MenuType { get; set; }
        [Required]
        public string MenuName { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public bool IsAwailable { get; set; }
    }
}
