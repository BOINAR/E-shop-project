using System.Threading.Tasks;
using Server.Models;

namespace Server.Repositories
{
    public interface ICartRepository
    {
        // Récupère le panier d'un utilisateur spécifique
        Task<Cart?> GetCartByUserIdAsync(int userId);

        // Met à jour le panier d'un utilisateur
        Task<bool> UpdateCartAsync(Cart cart);

        // Méthode pour créer un panier (si un utilisateur n'a pas encore de panier)
        Task<Cart> AddCartAsync(Cart cart);
    }
}