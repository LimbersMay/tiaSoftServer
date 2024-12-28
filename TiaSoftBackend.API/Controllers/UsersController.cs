using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROP;
using ROP.APIExtensions;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.DTOs.Users;
using TiaSoftBackend.UseCases.ErrorCodes;

namespace TiaSoftBackend.API.Controllers;

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

    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetUsers() {
        
        var users = await _userManager.Users.ToListAsync();
        var usersResponse = new List<UserDto>();
        
        users.Remove(users.FirstOrDefault(user => user.UserName == "superadmin@gmail.com"));
        
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userResponse = new UserDto()
            {
                UserId = user.Id,
                Username = user.FullName,
                Email = user.Email,
                Roles = roles.ToList()
            };
            usersResponse.Add(userResponse);
        }
        
        return Ok(usersResponse);
    }

    [HttpPut]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> UpdateUser([FromQuery] string userId, [FromBody] UpdateUserRequest updateUserDto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user is null)
        {
            return Result.BadRequest(ErrorCodes.UserNotFound).ToValueOrProblemDetails();
        }

        user.UserName = updateUserDto.Email;
        user.FullName = updateUserDto.Username;
        user.Email = updateUserDto.Email;

        // Update user
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return Result.BadRequest(ErrorCodes.UserErrorWhenUpdatingUSer).ToValueOrProblemDetails();
        }
        
        // Update user password
        if (updateUserDto.Password is not null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resultUpdatePassword = await _userManager.ResetPasswordAsync(user, token, updateUserDto.Password);

            if (!resultUpdatePassword.Succeeded)
            {
                foreach (var identityError in resultUpdatePassword.Errors)
                {
                    Console.WriteLine(identityError.Description);
                }
                return Result.Failure(ErrorCodes.UserErrorWhenUpdatingUSer).ToValueOrProblemDetails();
            }
        }
        
        // Update user roles
        var currentRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = new List<string>(updateUserDto.Roles);
        var rolesToRemove = new List<string>();
        
        // Delete current roles in rolesToAdd
        // To avoid adding roles that are already in the user
        foreach (var role in updateUserDto.Roles)
        {
            if (currentRoles.Contains(role))
            {
                rolesToAdd.Remove(role);
            }
        }

        foreach (var role in currentRoles)
        {
            if (!updateUserDto.Roles.Contains(role))
            {
                rolesToRemove.Add(role);
            }
        }
        
        // Remove roles
        if (rolesToRemove.Count > 0)
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded)
            {
                foreach (var removeResultError in removeResult.Errors)
                {
                    Console.WriteLine(removeResultError);
                }
                
                return Result.Failure(ErrorCodes.UserErrorWhenUpdatingUSer).ToValueOrProblemDetails();
            }
        }
        
        // Add roles
        if (rolesToAdd.Count > 0)
        {
            var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (!addResult.Succeeded)
            {
                foreach (var error in addResult.Errors)
                {
                    Console.WriteLine(error.Description);
                }
                
                return Result.Failure(ErrorCodes.UserErrorWhenUpdatingUSer).ToValueOrProblemDetails();
            }
        }
        
        var userRoles = await _userManager.GetRolesAsync(user);

        return Ok(new UserDto
        {
            UserId = user.Id,
            Username = user.FullName,
            Email = user.Email,
            Roles = userRoles.ToList()
        });
    }

    [HttpPost]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var emailExists = await _userManager.FindByEmailAsync(request.Email);
        if (emailExists is not null)
        {
            return Result.BadRequest(ErrorCodes.AuthErrorEmailAlreadyExists).ToValueOrProblemDetails();
        }

        var user = new User()
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.UserName,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return Result.Failure(ErrorCodes.UserErrorUserNotCreated).ToValueOrProblemDetails();
        }
        
        // Add password
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var addPasswordResult = await _userManager.ResetPasswordAsync(user, token, request.Password);
        
        if (!addPasswordResult.Succeeded)
        {
            foreach (var identityError in addPasswordResult.Errors)
            {
                Console.WriteLine(identityError.Description);
            }

            return Result.Failure(ErrorCodes.UserErrorWhenUpdatingUSer).ToValueOrProblemDetails();
        }
        
        // Add roles
        var roleResult = await _userManager.AddToRolesAsync(user, request.Roles);
        if (!roleResult.Succeeded)
        {
            foreach (var identityError in roleResult.Errors)
            {
                Console.WriteLine(identityError.Description);
            }
            
            return Result.Failure(ErrorCodes.UserErrorWhenUpdatingUSer).ToValueOrProblemDetails();
        }
        
        var userRoles = await _userManager.GetRolesAsync(user);
        
        return Ok(new UserDto
        {
            UserId = user.Id,
            Username = user.FullName,
            Email = user.Email,
            Roles = userRoles.ToList()
        });
    }
}   