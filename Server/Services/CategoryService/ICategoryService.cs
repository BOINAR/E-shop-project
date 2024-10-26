using Server.Models;

namespace Server.Services.CategoryService
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        Category AddCategory(Category newCategory);
        void RemoveCategory(int categoryId);
    }
}