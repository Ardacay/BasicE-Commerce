using AutoMapper;
using ECommerce.Dtos.Order;
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

        }
    }
}
