using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Repositories.CartItemRepository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly EcommerceDbContext _context;

        public CartItemRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem> AddAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task UpdateAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId)
        {
            return await _context.CartItems
                                 .Include(ci => ci.Product) // Inclure les détails du produit
                                 .Where(ci => ci.Id == cartId)
                                 .ToListAsync();
        }
    }
}