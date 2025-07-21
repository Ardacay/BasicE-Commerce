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
            CreateMap<Order,OrderDetailsDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDetailDto>()
                .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<OrderCreateDto,Order>();
            CreateMap<OrderItemDto,OrderItem>();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryController, Category>();


            CreateMap<Product, ProductDto>()
           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ProductCreateDto, Product>();
        }
    }
}
