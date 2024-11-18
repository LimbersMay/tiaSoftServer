using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.DTOs.Users;
using TiaSoftBackend.UseCases.ErrorCodes;

namespace TiaSoftBackend.API.Controllers;

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
    public async Task<IActionResult> SignIn(SignInUserRequest request)
    {
        var emailExists = await _userManager.FindByEmailAsync(request.Email);

        if (emailExists is not null)
        {
            return BadRequest(ErrorCodes.AuthErrorEmailAlreadyExists.ToString());
        }

        var user = new User
        {
            Email = request.Email,
            UserName = request.Email,
            FullName = request.UserName,
        };

        var result = _userManager.CreateAsync(user, request.Password).Result;

        if (!result.Succeeded)
        {
            return BadRequest(ErrorCodes.UserErrorUserNotCreated.ToString());
        }

        // Add roles
        var roleResult = await _userManager.AddToRolesAsync(user, ["Mesero"]);
        if (!roleResult.Succeeded)
        {
            // Return internal server error
            return BadRequest(ErrorCodes.UserErrorWhenUpdatingUSer.ToString());
        }

        await _signInManager.SignInAsync(user, isPersistent: true);

        var roles = await _userManager.GetRolesAsync(user);
        
        
        return new JsonResult(new UserDto
        {
            UserId = user.Id,
            Username = user.UserName,
            Email = user.Email,
            Roles = roles.ToList()
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password,
            isPersistent: true, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var roles = await _userManager.GetRolesAsync(user);

            return new JsonResult(new UserDto
            {
                UserId = user.Id,
                Username = user.FullName,
                Email = user.Email ?? string.Empty,
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

            return new JsonResult(new UserDto
            {
                UserId = user.Id,
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