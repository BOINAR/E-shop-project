using Server.Data;
using Server.Models;
using Microsoft.EntityFrameworkCore;
namespace Server.Repositories.CartRepository

{
    public class CartRepository : ICartRepository
    {
        private readonly EcommerceDbContext _context;
        public CartRepository(EcommerceDbContext context)
        {
            _context = context;
        }
        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
            return cart;
        }

        public async Task<bool> UpdateCartAsync(Cart cart)
        {
            var existingCart = await _context.Carts.FindAsync(cart.Id);
            if (existingCart == null) return false;

            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Cart> AddCartAsync(Cart cart)
        {
            var existingCart = await _context.Carts
        .FirstOrDefaultAsync(c => c.UserId == cart.UserId);

            if (existingCart != null)
            {
                return existingCart; // Retourne le panier existant s'il y en a un
            }

            _context.Add(cart);
            await _context.SaveChangesAsync();
            return cart;

        }
    }
}