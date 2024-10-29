namespace Server.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; } // référence à l'indentifiant de la Commande
        public Order? Orders { get; set; } // référence de navigation vers Order

        public int ProductId { get; set; }
        public Product? Products { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}