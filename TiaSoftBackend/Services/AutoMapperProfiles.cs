using AutoMapper;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Models.Menu;
using TiaSoftBackend.Models.Product;

namespace TiaSoftBackend.Services;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
    }
}