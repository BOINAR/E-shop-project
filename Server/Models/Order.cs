namespace Server.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Users? UserId { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }

    }
}