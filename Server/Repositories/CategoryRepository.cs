using Server.Data;
using Server.Models;
namespace Server.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly EcommerceDbContext? _context;
        public CategoryRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = _context?.Categories.ToList() ?? throw new Exception("Categories is null");

            return categories;
        }
        public Category GetById(int id)
        {
            var category = _context?.Categories.Find(id) ?? throw new Exception("Category is null");
            return category;

        }

        public void Add(Category entity)
        {
            _context?.Categories.Add(entity);


        }

        public void Update(Category entity)
        {
            var category = _context?.Categories.Update(entity) ?? throw new Exception("Category is null");
        }

        public void Delete(int id)
        {
            var category = GetById(id);
            _context?.Categories.Remove(category);
        }


    }
}