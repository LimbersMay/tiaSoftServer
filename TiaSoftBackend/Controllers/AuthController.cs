using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Models;

namespace TiaSoftBackend.controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    
    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("signIn")]
    public async Task<IActionResult> SignIn(CreateUserDto userDto)
    {
        
        var user = new IdentityUser()
        {
            Email = userDto.Email,
            UserName = userDto.UserName,
        };
        
        var result = _userManager.CreateAsync(user, userDto.Password).Result;
        
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: true);
            return Ok();
        }

        return BadRequest(result.Errors);
    }
}