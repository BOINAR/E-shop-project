
using Microsoft.VisualBasic;
using Server.Models;
using Server.Repositories.CategoryRepository;
namespace Server.Services.CategoryService
{


    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository? _categories;

        public CategoryService(ICategoryRepository categories)
        {
            _categories = categories;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            if (_categories == null) { throw new Exception("Categories is null"); }
            return await _categories.GetAllAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            if (_categories == null) { throw new Exception("Categories is null"); }
            var category = await _categories.GetByIdAsync(categoryId);
            return category;


        }
        public async Task<Category> AddCategory(Category newCategory)
        {

            if (_categories == null) { throw new Exception("Categories is null"); }
            await _categories.AddAsync(newCategory);
            return newCategory;

        }
        public async Task RemoveCategory(int categoryId)
        {
            if (_categories == null)
            {
                throw new Exception("Category is null");
            }
            await _categories.DeleteCategoryByIdAsync(categoryId);



        }
    }
}