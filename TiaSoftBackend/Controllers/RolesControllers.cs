using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Models;

namespace TiaSoftBackend.controllers;

[ApiController]
[Route("api/roles")]
public class RolesControllers: ControllerBase
{
    
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public RolesControllers(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet]
    [Authorize(Roles = "Administrador, SuperUser")]
    public IActionResult GetRoles()
    {
        var roles = _roleManager.Roles.Select(role => new RoleResponseDto()
        {
            RoleId = role.Id,
            Name = role.Name
        }).ToList();

        return Ok(roles);
    }
}