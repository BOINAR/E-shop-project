using Server.Models;

namespace Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task<Category> AddCategory(Category newCategory);
        void RemoveCategory(int categoryId);
    }
}