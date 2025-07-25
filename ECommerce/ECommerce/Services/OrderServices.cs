using AutoMapper;
using ECommerce.Dtos.Category;
using ECommerce.Dtos.Order;
using ECommerce.Dtos.Product;
using ECommerce.Models;
using ECommerce.Repositories;
using Humanizer;

namespace ECommerce.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public OrderServices(IRepository<Product> productRepository, IRepository<Order> orderRepository, IRepository<Category> categoryrepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _categoryRepository = categoryrepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            try
            {
                _orderRepository.Add(order);
            }
            catch (Exception e)
            {

            }
            _orderRepository.Save();

            return _mapper.Map<OrderDto>(order);

        }

        public async Task<OrderDto> DeleteOrderById(int id)
        {
            var order = await _orderRepository.GetIdAsync(id);
            if (order == null) throw new Exception("Order Not Found");
            _orderRepository.Remove(order);
            _orderRepository.Save();
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
           var order= await _orderRepository.GetIdAsync(id);
            if (order == null) throw new Exception("Order Not Found");
            return _mapper.Map<OrderDto>(order);

        }

        public async Task<OrderDto> UpdateOrder(OrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            if (order == null) throw new Exception("Order Not Found");
            _orderRepository.Update(order);
            _orderRepository.Save();
            return _mapper.Map<OrderDto>(order);
        }
    }
}
