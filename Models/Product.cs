using System.ComponentModel.DataAnnotations;
namespace B2CEcommerceApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; } = 0;
        public int CategoryId { get; set; } // Foreign key
        public Category? Category { get; set; } // Navigation property

    }
}
