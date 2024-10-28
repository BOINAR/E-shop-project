using Server.Models;
using Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Server.Repositories.ProductRepository
{

    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _context;

        public ProductRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}

