using System.Configuration;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

public static class RoleInitializer
{
    public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        string[] roleNames = {"SuperUsuario", "Gerente", "Capitan", "Mesero" };
        IdentityResult roleResult;
        
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        
        // Create a superuser who could maintain the web app
        var userFullName = configuration["SuperUser:UserFullName"];
        var userEmail = configuration["SuperUser:UserEmail"];
        var userPassword = configuration["SuperUser:UserPassword"];
        
        var poweruser = new User
        {
            FullName = userFullName,
            UserName = userEmail,
            Email = userEmail
        };
        
        if ( userPassword is null || userEmail is null || userFullName is null)
        {
            throw new ConfigurationErrorsException("SuperUser configuration is missing");
        }
        
        var user = await userManager.FindByEmailAsync(userEmail);
        if (user is null)
        {
            var createPowerUser = await userManager.CreateAsync(poweruser, userPassword);
            if (createPowerUser.Succeeded)
            {
                await userManager.AddToRoleAsync(poweruser, "SuperUsuario");
            }
        }
    }
}