using AutoMapper;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.DTOs.Users;

namespace TiaSoftBackend.UseCases.Mappers;

public class UsersProfile: Profile
{
    public UsersProfile()
    {
        // Users
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.FullName));
    }
}