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
        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == userId) ?? throw new Exception("Cart is null");
            return cart;
        }
    }
}