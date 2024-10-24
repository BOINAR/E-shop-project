
using Server.Models;
namespace Server.Services.CategoryService
{


    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _categories = new List<Category>();

        public IEnumerable<Category> GetAllCategories()
        {
            return _categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                throw new Exception($"Category with ID {categoryId} not found");
            }
            return category;
        }

        public Category CreateCategory(Category newCategory)
        {
            _categories.Add(newCategory);
            return newCategory;
        }
    }
}