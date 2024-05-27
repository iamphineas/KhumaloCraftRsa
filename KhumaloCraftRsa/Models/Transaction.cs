namespace KhumaloCraftRsa.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public string? UserId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public DateTime TransactionDate { get; set; }

        public virtual Product? Product { get; set; }

    }
}