using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.Data.Specifications.AreaSpecs;
using TiaSoftBackend.DTOs.Areas;

namespace TiaSoftBackend.UseCases.Areas;

public class UpdateArea (IAreasRepository areasRepository, IMapper mapper)
{
    public async Task<Result<AreaDto>> Execute(UpdateAreaRequest request, string areaId)
    {
        // Check if the area exists
        var area = await areasRepository.GetArea(new AreaIdSpecification(areaId));
        
        if (area == null)
        {
            return Result.NotFound<AreaDto>(ErrorCodes.ErrorCodes.AreaNotFound);
        }
        
        // Update the area
        area.Name = request.Name;
        area.Description = request.Description;
        
        var updatedArea = await areasRepository.UpdateArea(area);
        
        return mapper.Map<AreaDto>(updatedArea);
    }
}