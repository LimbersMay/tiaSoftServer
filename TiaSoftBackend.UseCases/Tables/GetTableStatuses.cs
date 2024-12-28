using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Tables;

namespace TiaSoftBackend.UseCases.Tables;

public class GetTableStatuses (ITableStatusesRepository tableStatusesRepository, IMapper mapper)
{
    public async Task<Result<List<TableStatusDto>>> Execute()
    {
        var tableStatuses = await tableStatusesRepository.GetTableStatuses();
        return mapper.Map<List<TableStatusDto>>(tableStatuses);
    }
}