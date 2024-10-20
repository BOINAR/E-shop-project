namespace Server.Models
{
    public class OrderItem {
        public int Id {get;set;}
        public Order? OrderId {get; set;}
        public Product? ProductId {get; set;}
        public int Quantity {get;set;}
    }
}