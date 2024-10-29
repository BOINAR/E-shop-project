using Server.Models;

namespace Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order newOrder); // créer un commande
        Task<Order> AddProductToOrder(int orderId, int productId);// Ajouter un produit
        Task<Order> RemoveProductFromOrder(int orderId, int productId); // supprimer un produit

        Task<IEnumerable<Order>> GetUserOrders(int userId);// Obtenir les commandes de l'utilisateur





    }
}