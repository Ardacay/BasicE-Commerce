using ECommerce.Dtos.Category;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryService;

        public CategoryController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = _categoryService.GetCategoryByIdAsync;
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategoryies(CategoryDto dto)
        {
            var categories = _categoryService.CreateCategory(dto);
            return Ok(categories);
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
          await  _categoryService.DeleteCategory(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id)
        {
            _categoryService.UpdateCategory(id);
            return NoContent();
        }



}
}
