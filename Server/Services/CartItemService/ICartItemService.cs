using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Services.CartItemService
{
    public interface ICartItemService
    {
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);
        Task<IEnumerable<CartItem>> GetCartItemByIdAsync(int cartId);
    }
}