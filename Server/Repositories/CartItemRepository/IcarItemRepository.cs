using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Repositories.CartItemRepository
{
    public interface ICartItemRepository
    {
        Task<CartItem> AddAsync(CartItem cartItem);
        Task UpdateAsync(CartItem cartItem);
        Task DeleteAsync(int cartItemId);
        Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId);
    }
}