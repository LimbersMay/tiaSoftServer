using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TiaSoftBackend.DTOs.Roles;

namespace TiaSoftBackend.UseCases.Mappers;

public class RolesProfile : Profile
{
    public RolesProfile()
    {
        CreateMap<IdentityRole, RoleDto>()
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}