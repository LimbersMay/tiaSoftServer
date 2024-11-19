using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.Data.Specifications;
using TiaSoftBackend.DTOs.Tables;

namespace TiaSoftBackend.UseCases.Tables;

public class GetTable (ITablesRepository tablesRepository, IMapper mapper)
{
    public async Task<Result<TableDto>> Execute(Specification<TableEntity> specification)
    {
        var result = await tablesRepository.GetTable(specification);
        
        if (result is null) 
            return Result.Failure<TableDto>(ErrorCodes.ErrorCodes.TableNotFound.ToString());
        
        
        return mapper.Map<TableDto>(result);
    }
}