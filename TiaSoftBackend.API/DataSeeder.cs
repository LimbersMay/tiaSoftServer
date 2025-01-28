using System.Configuration;
using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Data;
using TiaSoftBackend.Data.Constants;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Entities.OrderEntities;

namespace TiaSoftBackend.API;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class DataSeeder
{

    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    
    public DataSeeder(ApplicationDbContext context, IConfiguration configuration, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _context = context;
        _configuration = configuration;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task Seed()
    {
        await SeedRoles();
        await SeedTableStatuses();
        await SeedOrderStatuses();
        await SeedOrderProductStatuses();
    }
    
    private async Task SeedRoles()
    {
        string[] roleNames = {"SuperUsuario", "Gerente", "Capitan", "Mesero" };
        
        foreach (var roleName in roleNames)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        
        // Create a superuser who could maintain the web app
        var userFullName = _configuration["SuperUser:UserFullName"];
        var userEmail = _configuration["SuperUser:UserEmail"];
        var userPassword = _configuration["SuperUser:UserPassword"];
        
        if ( userPassword is null || userEmail is null || userFullName is null)
        {
            throw new ConfigurationErrorsException("SuperUser configuration is missing");
        }
        
        var powerUser = new User
        {
            FullName = userFullName,
            UserName = userEmail,
            Email = userEmail
        };
        
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user is null)
        {
            var createPowerUser = await _userManager.CreateAsync(powerUser, userPassword);
            if (createPowerUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(powerUser, "SuperUsuario");
            }
        }
    }

    private async Task SeedTableStatuses()
    {
        var hasTableStatuses = await _context.TableStatuses.AnyAsync();
        
        if (hasTableStatuses)
        {
            return;
        }
        
        var tableStatuses = new List<TableStatus>
        {
            new ()
            {
                Name = TableStatusConstants.Activo.ToString(), 
                Description = "Mesa disponible para ser ocupada", 
                TableStatusId = Guid.NewGuid().ToString()
            },
            new ()
            {
                Name = TableStatusConstants.PorAutorizar.ToString(),
                Description = "Mesa ocupada y en espera de autorización del pago",
            },
            new ()
            {
                Name = TableStatusConstants.Pagado.ToString(),
                Description = "Mesa pagada y disponible para ser ocupada",
            }
        };
        
        await _context.TableStatuses.AddRangeAsync(tableStatuses);
        await _context.SaveChangesAsync();
    }

    private async Task SeedOrderStatuses()
    {
        var hasOrderStatuses = await _context.OrderStatuses.AnyAsync();
        if (hasOrderStatuses)
        {
            return;
        }

        var orderStatuses = new List<OrderStatus>()
        {
            new()
            {
                Name = OrderStatusConstants.Activo.ToString(),
                Description = "Orden activa",
                OrderStatusId = Guid.NewGuid().ToString()
            },
            new ()
            {
                Name = OrderStatusConstants.Cancelado.ToString(),
                Description = "Orden cancelada, (todos los platillos de la orden fueron cancelados)",
                OrderStatusId = Guid.NewGuid().ToString()
            }
        };
        
        await _context.OrderStatuses.AddRangeAsync(orderStatuses);
        await _context.SaveChangesAsync();
    }

    private async Task SeedOrderProductStatuses()
    {
        var hasOrderProductStatuses = await _context.OrderProductStatuses.AnyAsync();
        
        if (hasOrderProductStatuses)
        {
            return;
        }
        
        var orderProductStatuses = new List<OrderProductStatus>
        {
            new ()
            {
                Name = "Activo", 
                Value = "Activo", 
                Description = "Platillo activo en la orden", 
                OrderProductStatusId = Guid.NewGuid().ToString()
            },
            new ()
            {
                Name = "Cancelado", 
                Value = "Cancelado", 
                Description = "Platillo cancelado en la orden", 
                OrderProductStatusId = Guid.NewGuid().ToString()
            },
        };
        
        await _context.OrderProductStatuses.AddRangeAsync(orderProductStatuses);
        await _context.SaveChangesAsync();
    }
}