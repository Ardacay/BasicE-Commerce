using ECommerceManager.Dtos.Categories;
using ECommerceManager.Dtos.Order;
using ECommerceManager.İnterfaces;
using ECommerceManager.Managers;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _orderManager.CreateAsync(dto);
            return RedirectToAction("Index");
        }

    }
}
