using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Order;
using ECommerceManager.İnterfaces;
using ECommerceManager.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceManager.Controllers
{
    public class OrderController: Controller
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderManager.GetAllAsync();
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderManager.GetByIdAsync(id);
            return View(order);
        }
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var createdOrder = await _orderManager.CreateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderManager.GetByIdAsync(id);
            return View(order);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderManager.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderManager.GetByIdAsync(id);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OrderDto dto) 
        {
            if (!ModelState.IsValid)
                return View( dto);

            await _orderManager.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

    }
}
