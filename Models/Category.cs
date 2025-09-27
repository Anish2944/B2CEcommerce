namespace B2CEcommerceApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property 1 category to many products
        public ICollection<Product> Products { get; set; } = []; 
    }
}
