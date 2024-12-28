using AutoMapper;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.DTOs.Areas;

namespace TiaSoftBackend.UseCases.Mappers;

public class AreasProfile: Profile
{
    public AreasProfile()
    {
        CreateMap<Area, AreaDto>();
        CreateMap<CreateAreaRequest, Area>();
        CreateMap<UpdateAreaRequest, Area>();
    }
}