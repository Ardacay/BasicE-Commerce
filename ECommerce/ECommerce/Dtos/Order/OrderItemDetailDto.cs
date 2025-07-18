namespace ECommerce.Dtos.Order
{
    public class OrderItemDetailDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
