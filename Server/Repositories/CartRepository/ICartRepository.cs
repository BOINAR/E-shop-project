using Server.Models;

namespace Server.Repositories.CartRepository
{
   public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
    }
}