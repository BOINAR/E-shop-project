namespace Server.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // Clé étrangère pour l'utilisateur
        public User? User { get; set; }  // Propriété de navigation vers l'utilisateur
        public ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();  // Liste des articles dans le panier
    }
}