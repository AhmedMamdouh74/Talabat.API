using Application.DTOs.Order;
using Application.DTOs.Product;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Order_Aggregate;

namespace Application.Mapping
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<Product, ReadProductDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlReslover>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<OrderItemDto,OrderItem>().ReverseMap();

            CreateMap<Order, ReadOrderDto>()
               .ForMember(dest => dest.ShipToAddress, opt => opt.MapFrom(src => src.ShipToAddress))
               .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
               .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
               .ForMember(dest => dest.BuyerEmail, opt => opt.MapFrom(src => src.BuyerEmail))
               .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal))
               .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod))
               .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal())).ReverseMap();

        }
    }
}
