using AutoMapper;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.DTOs.Orders;

namespace TiaSoftBackend.UseCases.Mappers;

public class OrdersProfile : Profile
{
    public OrdersProfile()
    {
        // Order
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Table.Name));
        
        CreateMap<OrderProduct, OrderProductDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        
        CreateMap<OrderStatus, OrderStatusDto>();
    }
}