using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend.controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    
    private readonly UserManager<User> _userManager;

    public UsersController(UserManager<User> userManager){
        _userManager = userManager;
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("EmailExists")]
    public async Task<IActionResult> EmailExists([FromQuery] string email)
    {

        Console.WriteLine(email);
        
        var emailExists = await _userManager.FindByEmailAsync(email);
        
        if (emailExists != null)
        {
            return Ok(true);
        }
        
        return Ok(false);
    }
    
}   