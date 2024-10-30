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
            var cart = await _context.Cart.FirstOrDefaultAsync(c => c.Id == userId);
            return cart;
        }

        public async Task<bool> UpdateCartAsync(Cart cart)
        {
            _context.Cart.Update(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Cart> AddCartAsync(Cart cart)
        {
            _context.Add(cart);
            await _context.SaveChangesAsync();
            return cart;

        }
    }
}