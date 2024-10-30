namespace Server.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public User? Users { get; set; }
        public ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();

    }
}