using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Areas;

namespace TiaSoftBackend.UseCases.Areas;

public class CreateArea (IAreasRepository areasRepository, IMapper mapper)
{
    public async Task<Result<AreaDto>> Execute(CreateAreaRequest request)
    {
        var areaEntity = mapper.Map<Area>(request);
        
        areaEntity.AreaId = Guid.NewGuid().ToString();
        
        var area = await areasRepository.CreateArea(areaEntity);
        
        return mapper.Map<AreaDto>(area);
    }
}