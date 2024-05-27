namespace KhumaloCraftRsa.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string? UserId { get; set; }

        public int? Quantity { get; set; }

        public DateTime OrderDate { get; set; }

        public int? ProductID { get; set; }

        public string? ProductName { get; set; }

        public bool Approved { get; set; }

      

    }
}