using Server.Models;

namespace Server.Services.CartService
{
   public interface ICartService
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
    }
}