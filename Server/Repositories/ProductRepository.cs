using Server.Models;
using Server.Data;

namespace Server.Repositories
{
    // Repositories/CategoryRepository.cs
    public class ProductRepository : IRepository<Product>
    {
        private readonly EcommerceDbContext _context;
        public ProductRepository(EcommerceDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAll() => _context.Products.ToList();
        public Product GetById(int id) => _context?.Products.Find(id) ?? throw new Exception("Product is null");
        public void Add(Product entity) => _context.Products.Add(entity);
        public void Update(Product entity) => _context.Products.Update(entity);
        public void Delete(int id)
        {
            var product = GetById(id);
            _context.Products.Remove(product);
        }
    }
}

