using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Constants;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Tables;

namespace TiaSoftBackend.UseCases.Tables;

public class CreateTable (ITablesRepository tablesRepository, ITableStatusesRepository tableStatusesRepository, IMapper mapper)
{
    public async Task<Result<TableDto>> Execute(CreateTableRequest request, string waiterId)
    {
        // Default status for a new table
        var activeStatus = await tableStatusesRepository.GetTableStatusByName(TableStatusConstants.Activo.ToString());
        
        var newTable = new TableEntity()
        {
            TableId = Guid.NewGuid().ToString(),
            Name = request.Name,
            AreaId = request.AreaId,
            UserId = waiterId,
            TableStatusId = activeStatus.TableStatusId
        };
        
        var result = await tablesRepository.CreateTable(newTable);
        
        return mapper.Map<TableDto>(result);
    }
}