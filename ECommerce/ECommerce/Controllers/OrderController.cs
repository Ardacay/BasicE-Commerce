using ECommerce.Dtos.Order;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpPost]
        //json formatında dto almak için frombody kullanılır
        public async Task<IActionResult> Create([FromBody] OrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _orderServices.CreateOrderAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          var order =await _orderServices.GetAllOrders();
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderServices.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           await _orderServices.DeleteOrderById(id);
            return Ok(id);

        }
        [HttpPost]
        public async Task<IActionResult> Update(OrderDto dto)
        {
            var order = await _orderServices.UpdateOrder(dto);
            return Ok(order);
        }

    }
}
