namespace B2CEcommerceApp.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; } // Navigation property
        public int Quantity { get; set; } = 1;
        public string UserId { get; set; } = string.Empty; // To associate cart items with a specific user
    }
}
