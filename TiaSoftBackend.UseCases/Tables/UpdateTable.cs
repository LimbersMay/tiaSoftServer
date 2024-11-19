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
            return Result.Failure<TableDto>(ErrorCodes.ErrorCodes.TableNotFound.ToString());
        }

        var tableEntity = new TableEntity
        {
            TableId = tableId,
            Name = request.Name,
            AreaId = request.AreaId,
            UserId = table.UserId,
            TableStatusId = table.TableStatusId
        };
        
        var result = await tablesRepository.UpdateTable(tableEntity);
        
        return mapper.Map<TableDto>(result);
    }
}