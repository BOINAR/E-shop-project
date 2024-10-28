using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.CategoryService;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }
        [HttpGet("categoryId")]
        public ActionResult<Category> GetCategoryById(int categoryId)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            return Ok(category);
        }
        [HttpPost]
        public ActionResult<Category> AddCategory([FromBody] Category newCategory)
        {
            var category = _categoryService.AddCategory(newCategory);
            return Ok(category);
        }
    }



}