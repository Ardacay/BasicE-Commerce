using AutoMapper;
using ECommerce.Controllers;
using ECommerce.Dtos.Category;
using ECommerce.Dtos.Order;
using ECommerce.Dtos.Product;
using ECommerce.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace ECommerce.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          
            CreateMap<Order, OrderDto>().ReverseMap();  
           
            CreateMap<OrderDto,OrderItemDto >().ReverseMap();
           
      
            CreateMap<OrderItemDto, OrderProduct>();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryController, Category>();




            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>().ReverseMap();
        }
    }
}
