using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP.APIExtensions;
using TiaSoftBackend.DTOs.Areas;
using TiaSoftBackend.UseCases.Areas;

namespace TiaSoftBackend.API.Controllers;

[ApiController]
[Route("api/areas")]
public class AreasController(AreasUseCases areas) : ControllerBase
{

    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetAreas()
        => await areas.GetAreas.Execute()
            .ToValueOrProblemDetails();

    [HttpPost]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> CreateArea([FromBody] CreateAreaRequest request)
        => await areas.CreateArea.Execute(request)
            .ToValueOrProblemDetails();

    [HttpPut("{areaId}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> UpdateArea(string areaId, [FromBody] UpdateAreaRequest request)
        => await areas.UpdateArea.Execute(request, areaId)
            .ToValueOrProblemDetails();
}