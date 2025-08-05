using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Order;
using ECommerceManager.İnterfaces;
using ECommerceManager.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var json = JsonConvert.SerializeObject(await _categoryManager.GetAllAsync());
            return Content(json, "application/json");
        }

        public async Task<IActionResult> Details(int id)
        {

            var categoryDetails = await _categoryManager.GetByIdAsync(id);
            return View(categoryDetails);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryDetails(int id)
        {

            var json = JsonConvert.SerializeObject(await _categoryManager.GetByIdAsync(id));
            return Content(json, "application/json");
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _categoryManager.GetByIdAsync(id);
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDto Dto)
        {
            if (!ModelState.IsValid) return View(Dto.Id.ToString(), Dto);
            await _categoryManager.UpdateAsync(Dto);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var categories = await _categoryManager.GetByIdAsync(id);
            return View(categories);
        }
        public async Task<IActionResult> DeleteById(int id)
        {
            var json = JsonConvert.SerializeObject(await _categoryManager.GetByIdAsync(id));
            return Content(json,"application/json");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryManager.DeleteAsync(id);
            return RedirectToAction("Index");
        }




    }
}
