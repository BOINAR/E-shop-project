namespace Server.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Users? Users { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }

    }
}