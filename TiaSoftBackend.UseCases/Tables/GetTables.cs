using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.Data.Specifications;
using TiaSoftBackend.DTOs.Tables;

namespace TiaSoftBackend.UseCases.Tables;

public class GetTables (ITablesRepository tablesRepository, IMapper mapper)
{
    public async Task<Result<List<TableDto>>> Execute(Specification<TableEntity> specification)
    {
        var result = await tablesRepository.GetTables(specification);

        return mapper.Map<List<TableDto>>(result);
    }
}