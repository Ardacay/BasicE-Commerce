using AutoMapper;
using ECommerce.Dtos.Order;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        public OrderServices(IRepository<Product> productRepository, IRepository<Order> orderRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDetailsDto> CreateOrderAsync(OrderCreateDto orderDto)
        {
            var orderItems = new List<OrderItem>();
            foreach(var itemdto in orderDto.Items)
            {
                var product=await _productRepository.GetIdAsync(itemdto.ProductId);
                if (product != null)    
                    throw new Exception("product not found");
                if (product.Stock < itemdto.Quantity)
                    throw new Exception($"'{product.Name}'stok yetersiz");

                product.Stock -= itemdto.Quantity;
                var ordeItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemdto.Quantity,
                    UnitPrice = product.Price,
                };
                orderItems.Add(ordeItem);
                _productRepository.Add(product);
            }
            var order = new Order
            {
                CustomerName = orderDto.CustomerName,
                OrderDate = DateTime.Now,
                OrderItems = orderItems

            };
            _orderRepository.Add(order);
            _orderRepository.Save();
            _productRepository.Save();

            return _mapper.Map<OrderDetailsDto>(order);
        }
        public async Task<OrderDetailsDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetIdAsync(id);
            return _mapper.Map<OrderDetailsDto>(order);
        }
    }
}
