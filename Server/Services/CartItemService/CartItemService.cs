using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;
using Server.Repositories;
using Server.Repositories.CartItemRepository;

namespace Server.Services.CartItemService
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItem;

        public CartItemService(ICartItemRepository cartItem)
        {
            _cartItem = cartItem;
        }

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            // Logique supplémentaire (ex: vérifier la disponibilité du produit) peut être ajoutée ici
            return await _cartItem.AddAsync(cartItem);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            await _cartItem.UpdateAsync(cartItem);
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
            await _cartItem.DeleteAsync(cartItemId);
        }

        public async Task<IEnumerable<CartItem>> GetCartItemById(int cartItemId)
        {
            return await _cartItem.GetCartItemByIdAsync(cartItemId);
        }
    }
}