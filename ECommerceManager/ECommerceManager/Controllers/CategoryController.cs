using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Order;
using ECommerceManager.İnterfaces;
using ECommerceManager.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceManager.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryManager.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var categories = await _categoryManager.GetByIdAsync(id);
            return View(categories);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto Dto)
        {
            if (!ModelState.IsValid) return View(Dto);
            await _categoryManager.CreateAsync(Dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _categoryManager.GetByIdAsync(id);
            return View(categories);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(int id, CategoryDto Dto)
        {
            if (!ModelState.IsValid) return View(id.ToString(), Dto);
            await _categoryManager.UpdateAsync(id, Dto);
            return RedirectToAction("Index");
            
        }

        public IActionResult Delete(int id)
        {
            var categories = _categoryManager.GetByIdAsync(id);
            return View(categories);
        }
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryManager.DeleteAsync(id);
            return RedirectToAction("Index");
        }




    }
}
