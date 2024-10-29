using Server.Models;
using Server.Repositories.CartRepository;

namespace Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ICartRepository? _Cart;

        public CartService(ICartRepository cart)
        {
            _Cart = cart;
        }
        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            if (_Cart == null) { throw new Exception("Cart is null"); }
            return await _Cart.GetCartByUserIdAsync(userId);
        }

    }
}