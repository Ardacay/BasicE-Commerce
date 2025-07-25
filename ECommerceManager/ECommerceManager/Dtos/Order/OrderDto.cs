using ECommerceManager.Models;

namespace ECommerceManager.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }

        public int CategoryId { get; set; }
  
        public List<OrderItemDto>? Items { get; set; }

    }
}
