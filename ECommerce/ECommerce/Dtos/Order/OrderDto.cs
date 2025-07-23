namespace ECommerce.Dtos.Order
{
    public class OrderDto
    {
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
