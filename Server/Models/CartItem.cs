namespace Server.Models
{
    public class CartItem {
        public int Id {get;set;}
        public Cart? CartId {get;set;}
        public Product? Product {get;set;}
        public int Quantity {get; set;}
    }
}