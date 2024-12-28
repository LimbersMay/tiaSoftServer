using AutoMapper;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.DTOs.Bills;
using TiaSoftBackend.DTOs.Tables;

namespace TiaSoftBackend.UseCases.Mappers;

public class BillsProfile: Profile
{
    public BillsProfile()
    {
        CreateMap<Bill, BillDto>();
        CreateMap<TableStatus, TableStatusDto>();
    }
}