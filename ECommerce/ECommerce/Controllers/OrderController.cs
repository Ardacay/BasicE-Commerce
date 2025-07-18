using ECommerce.Dtos.Order;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController:ControllerBase
    {
        private readonly IOrderServices _orderServices;
        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        //{
        //    if(!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    try
        //    {
        //        var result = await _orderServices.CreateOrderAsync(dto);
        //        return CreatedAtAction
        //    }
        //}
    }
}
