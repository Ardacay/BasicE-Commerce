using ECommerceManager.Dtos.Product;
using ECommerceManager.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceManager.Controllers
{
    public class ProductController: Controller
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productManager.GetAllAsync();
            return View(products);
        }

        public IActionResult Details()
        {
            var product= _productManager.GetAllAsync();
            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _productManager.CreateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productManager.GetByIdAsync(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _productManager.UpdateAsync(id, dto);
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
