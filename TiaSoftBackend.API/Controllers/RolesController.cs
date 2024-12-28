using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.DTOs.Roles;

namespace TiaSoftBackend.API.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesControllers (RoleManager<IdentityRole> roleManager, IMapper mapper) : ControllerBase
{

    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public IActionResult GetRoles()
    {
        var roles = mapper.Map<IEnumerable<RoleDto>>(roleManager.Roles);

        return Ok(roles);
    }
}