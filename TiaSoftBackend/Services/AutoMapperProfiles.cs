using AutoMapper;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Models;
using TiaSoftBackend.Models.Area;
using TiaSoftBackend.Models.Menu;
using TiaSoftBackend.Models.Product;
using TiaSoftBackend.Models.Table;

namespace TiaSoftBackend.Services;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
        
        // Areas
        CreateMap<Area, AreaResponseDto>();
        CreateMap<CreateAreaDto, Area>();
        CreateMap<UpdateAreaDto, Area>();
        
        // Users
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.FullName));

        // TableStatuses
        CreateMap<TableStatus, TableStatusResponseDto>();
        
        // Tables
        CreateMap<TableEntity, TableResponseDto>();
    }
}