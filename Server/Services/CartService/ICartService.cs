using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Services.CartService
{
    public interface ICartService
    {
        // Récupérer le panier d'un utilisateur spécifique
        Task<Cart?> GetCartByUserIdAsync(int userId);

        // Ajouter un article au panier
        Task<bool> AddItemToCart(Cart cart, CartItem cartItem);

        // Mettre à jour un article dans le panier
        Task<bool> UpdateCartItem(int userId, int cartItemId, CartItem updatedCartItem);

        // Supprimer un article du panier
        Task<bool> RemoveItemFromCart(int cartItemId);

        // Calculer le total du panier
        Task<decimal> CalculateCartTotal(int userId);
    }
}