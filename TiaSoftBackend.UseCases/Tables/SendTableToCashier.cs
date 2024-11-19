using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Constants;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.Data.Specifications.TableSpecs;
using TiaSoftBackend.DTOs.Tables;

namespace TiaSoftBackend.UseCases.Tables;

public class SendTableToCashier (ITablesRepository tablesRepository, ITableStatusesRepository tableStatusesRepository, IMapper mapper)
{
    public async Task<Result<TableDto>> Execute(string tableId)
    {
        var table = await tablesRepository.GetTable(new TableIdSpecification(tableId));

        // If the table is not found, return an error
        if (table is null)
            return Result.Failure<TableDto>(ErrorCodes.ErrorCodes.TableNotFound.ToString());
        
        var tableStatus = await tableStatusesRepository.GetTableStatusByName(TableStatusConstants.PorAutorizar.ToString());

        // If the table status is not found, return an error
        if (tableStatus is null) 
            return Result.Failure<TableDto>(ErrorCodes.ErrorCodes.TableStatusNotFound.ToString());

        var tableEntity = new TableEntity
        {
            TableId = tableId,
            TableStatusId = tableStatus.TableStatusId,
            Name = table.Name,
            AreaId = table.AreaId,
            UserId = table.UserId,
        };
        
        var result = await tablesRepository.UpdateTable(tableEntity);
        
        return mapper.Map<TableDto>(result);
    }
}