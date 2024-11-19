using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.Data.Specifications.TableSpecs;
using TiaSoftBackend.DTOs.Tables;

namespace TiaSoftBackend.UseCases.Tables;

public class UpdateTable (ITablesRepository tablesRepository, IMapper mapper)
{
    public async Task<Result<TableDto>> Execute(string tableId, UpdateTableRequest request)
    {
        var table = await tablesRepository.GetTable(new TableIdSpecification(tableId));

        if (table is null)
        {
            return Result.NotFound<TableDto>(ErrorCodes.ErrorCodes.TableNotFound.ToString());
        }
        
        table.Name = request.Name;
        table.AreaId = request.AreaId;
        
        var result = await tablesRepository.UpdateTable(table);
        
        return mapper.Map<TableDto>(result);
    }
}