using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Areas;

namespace TiaSoftBackend.UseCases.Areas;

public class GetAreas (IAreasRepository areasRepository, IMapper mapper)
{
    public async Task<Result<List<AreaDto>>> Execute()
    {
        var areas = await areasRepository.GetAreas();
        
        return mapper.Map<List<AreaDto>>(areas);
    }
}