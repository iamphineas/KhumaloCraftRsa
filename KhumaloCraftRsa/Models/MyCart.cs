namespace KhumaloCraftRsa.Models
{
    public class MyCart
    {
        public int MyCartId { get; set; }

        public string? UserId { get; set; }

        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }

    }
}