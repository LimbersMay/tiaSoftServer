using AutoMapper;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.DTOs.Tables;

namespace TiaSoftBackend.UseCases.Mappers;

public class TablesProfile: Profile
{
    public TablesProfile()
    {
        CreateMap<TableEntity, TableDto>();
    }
}