using AutoMapper;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.DTOs.Menu;

namespace TiaSoftBackend.UseCases.Mappers;

public class MenusProfile: Profile
{
    public MenusProfile()
    {
        CreateMap<MenuDto, Product>();
        
        CreateMap<UpdateMenuRequest, Product>();
        
        CreateMap<Product, MenuDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
    }
}