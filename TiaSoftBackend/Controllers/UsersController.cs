using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Enums;
using TiaSoftBackend.Models;

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

    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetUsers() {
        
        var users = await _userManager.Users.ToListAsync();
        var usersResponse = new List<UserResponseDto>();
        
        users.Remove(users.FirstOrDefault(user => user.UserName == "superadmin@gmail.com"));
        
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userResponse = new UserResponseDto
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
    public async Task<ActionResult> UpdateUser([FromQuery] string userId, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user is null)
        {
            return NotFound(ErrorCodes.UserNotFound.ToString());
        }

        user.UserName = updateUserDto.Email;
        user.FullName = updateUserDto.Username;
        user.Email = updateUserDto.Email;

        // Update user
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(ErrorCodes.UserNotUpdated.ToString());
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
                return BadRequest(ErrorCodes.UserErrorWhenUpdatingUSer.ToString());
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
                
                return BadRequest(ErrorCodes.UserErrorWhenUpdatingUSer.ToString());
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
                
                return BadRequest(ErrorCodes.UserErrorWhenUpdatingUSer.ToString());
            }
        }
        
        var userRoles = await _userManager.GetRolesAsync(user);

        return Ok(new UserResponseDto()
        {
            UserId = user.Id,
            Username = user.FullName,
            Email = user.Email,
            Roles = userRoles.ToList()
        });
    }

    [HttpPost]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var emailExists = await _userManager.FindByEmailAsync(createUserDto.Email);
        if (emailExists is not null)
        {
            return BadRequest(ErrorCodes.AuthErrorEmailAlreadyExists.ToString());
        }

        var user = new User()
        {
            UserName = createUserDto.Email,
            Email = createUserDto.Email,
            FullName = createUserDto.UserName,
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(ErrorCodes.UserErrorUserNotCreated.ToString());
        }
        
        // Add password
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var addPasswordResult = await _userManager.ResetPasswordAsync(user, token, createUserDto.Password);
        
        if (!addPasswordResult.Succeeded)
        {
            foreach (var identityError in addPasswordResult.Errors)
            {
                Console.WriteLine(identityError.Description);
            }

            return BadRequest(ErrorCodes.UserErrorWhenCreatingUser.ToString());
        }
        
        // Add roles
        var roleResult = await _userManager.AddToRolesAsync(user, createUserDto.Roles);
        if (!roleResult.Succeeded)
        {
            foreach (var identityError in roleResult.Errors)
            {
                Console.WriteLine(identityError.Description);
            }
            
            return BadRequest(ErrorCodes.UserErrorWhenCreatingUser.ToString());
        }
        
        var userRoles = await _userManager.GetRolesAsync(user);
        
        return Ok(new UserResponseDto()
        {
            UserId = user.Id,
            Username = user.FullName,
            Email = user.Email,
            Roles = userRoles.ToList()
        });
    }
}   