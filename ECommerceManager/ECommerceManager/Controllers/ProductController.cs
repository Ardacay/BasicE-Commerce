using ECommerceManager.Dtos.Product;
using ECommerceManager.Managers;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceManager.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager;

        public ProductController(IProductManager productManager, ICategoryManager categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productManager.GetAllAsync();
            var categories = await _categoryManager.GetAllAsync();
            ViewBag.Categories = categories.ToDictionary(c => c.Id, c => c.Name);
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productManager.GetByIdAsync(id);
            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var createdProduct = await _productManager.CreateAsync(dto);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productManager.GetByIdAsync(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto.Id.ToString(), dto);

            await _productManager.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productManager.GetByIdAsync(id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productManager.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
