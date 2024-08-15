using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Enums;
using TiaSoftBackend.Models;

namespace TiaSoftBackend.controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    
    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("signIn")]
    public async Task<IActionResult> SignIn(CreateUserDto userDto)
    {
        var emailExists = await _userManager.FindByEmailAsync(userDto.Email);
        
        if (emailExists != null)
        {
            return BadRequest(ErrorCodes.AuthErrorEmailAlreadyExists.ToString());
        }
        
        var user = new User()
        {
            Email = userDto.Email,
            UserName = userDto.Email,
            FullName = userDto.UserName,
        };
        
        var result = _userManager.CreateAsync(user, userDto.Password).Result;
        
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: true);

            var roles = await _userManager.GetRolesAsync(user);
            return new JsonResult(new UserResponseDto
            {
                Username = user.UserName,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }

        return new JsonResult(result.Errors);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserDto loginUser)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, 
            isPersistent:true, lockoutOnFailure:false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            var roles = await _userManager.GetRolesAsync(user);

            return new JsonResult(new UserResponseDto()
            {
                Username = user.FullName,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }
        
        // Unauthorized
        return Unauthorized(ErrorCodes.AuthErrorIncorrectCredentials.ToString());
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("isAuthenticated")]
    public IActionResult IsAuthenticated()
    {
        var isAuthenticated = _signInManager.IsSignedIn(User);

        if (isAuthenticated)
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (user is null)
            {
                return Unauthorized(ErrorCodes.AuthErrorNotAuthorized.ToString());
            }
            
            var roles = _userManager.GetRolesAsync(user).Result;
            
            return new JsonResult(new UserResponseDto()
            {
                Username = user.FullName,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }

        return Unauthorized(ErrorCodes.AuthErrorNotAuthorized.ToString());
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("logout")]
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync();
        return Ok(true);
    }
}