using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ROP;
using ROP.APIExtensions;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.UseCases.ErrorCodes;
using TiaSoftBackend.UseCases.Orders;
using TiaSoftBackend.Data.Specifications.OrderSpecs;

namespace TiaSoftBackend.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController (OrdersUseCases orders, UserManager<User> userManager): ControllerBase
{
    
    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan, Mesero")]
    public async Task<IActionResult> GetOrders()
    {
        /*
         * Depending on the role of the user, the orders will be filtered by the user's id
         */
        
        var userId = userManager.GetUserId(User);

        if (userId is null)
        {
            return Result.NotFound<string>(ErrorCodes.UserNotFound)
                .ToValueOrProblemDetails();
        }

        Console.WriteLine($"User id: {userId}");

        return await orders.GetOrders.Execute(new UserIdSpecification(userId))
            .ToValueOrProblemDetails();
    }
    
    [HttpGet("{tableId:required}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan, Mesero")]
    public async Task<IActionResult> GetOrdersByTableId(string tableId)
    {
        return await orders.GetOrders.Execute(new TableIdSpecification(tableId))
            .ToValueOrProblemDetails();
    }
}