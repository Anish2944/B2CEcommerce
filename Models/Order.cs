namespace B2CEcommerceApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; // To associate orders with a specific user
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public string ShippingAddress { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string PaymentMethod { get; set; } = "Cash on Delivery";

        public ICollection<OrderItem> OrderItems { get; set; } = []; // Navigation property
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; } // Navigation property
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; } // Foreign key
        public Order? Order { get; set; } // Navigation property
    }
}
