using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Models.Area;
using TiaSoftBackend.Services;

namespace TiaSoftBackend.controllers;

[ApiController]
[Route("api/areas")]
public class AreasController: ControllerBase
{
    private readonly IAreasRepository _areasRepository;
    private readonly IMapper _mapper;

    public AreasController(IAreasRepository areasRepository, IMapper mapper)
    {
        _areasRepository = areasRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetAreas()
    {
        var areas = await _areasRepository.GetAreas();
        var areasDto = _mapper.Map<IEnumerable<AreaResponseDto>>(areas);
        
        return new JsonResult(areasDto);
    }
    
    [HttpPost]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> CreateArea([FromBody] CreateAreaDto createAreaDto)
    {
        var area = _mapper.Map<Area>(createAreaDto);
        area.AreaId = Guid.NewGuid().ToString();
        var areaResult = await _areasRepository.CreateArea(area);
        
        return new JsonResult(_mapper.Map<AreaResponseDto>(areaResult));
    }
    
    [HttpPut("{areaId}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> UpdateArea(string areaId, [FromBody] UpdateAreaDto updateAreaDto)
    {
        var area = _mapper.Map<Area>(updateAreaDto);
        area.AreaId = areaId;
        
        var areaResult = await _areasRepository.UpdateArea(area);
        
        return new JsonResult(_mapper.Map<AreaResponseDto>(areaResult));
    }
}