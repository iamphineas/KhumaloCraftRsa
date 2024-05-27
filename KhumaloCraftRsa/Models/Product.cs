namespace KhumaloCraftRsa.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;

        public bool Availability { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<MyCart> MyCarts { get; set; } = new List<MyCart>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}