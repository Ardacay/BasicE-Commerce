namespace ECommerce.Dtos.Order
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDetailDto> Items { get; set; }

    }
}
