using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TiaSoftBackend.DTOs.Orders;
using TiaSoftBackend.UseCases.ErrorCodes;
using TiaSoftBackend.UseCases.Orders;

namespace TiaSoftBackend.API.Hubs;

public interface IOrderHub
{
    Task ReceiveOrders(List<OrderDto> orders);
    Task ReceiveError(ProblemDetails problemDetails);
}

[Authorize]
public class OrderHub (OrdersUseCases orders): Hub<IOrderHub>
{
    
    public override async Task OnConnectedAsync()
    {
        var userRoles = Context.User?.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();
        
        // Roles to be in all tables
        // If the user has any of these roles, they will be added to the "Tables" group
        var roles = new List<string> { "SuperUsuario", "Gerente", "Capitan", "Mesero" };

        if (userRoles != null)
        {
            if (userRoles.Intersect(roles).Any())
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "ManagersAndCaptains");
            }
        }
        
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "ManagersAndCaptains");
        await base.OnDisconnectedAsync(exception);
    }
    
    public async Task CreateOrder(CreateOrderRequest request, string areaId)
    {
        var userId = Context.UserIdentifier;

        if (userId is null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = ErrorCodes.UserNotFound,
                Detail = "User not found",
                Status = 404,
                Extensions =
                {
                    { ErrorCodes.UserNotFound, ErrorCodes.UserNotFound }
                }
            };
            
            await Clients.User(Context.ConnectionId).ReceiveError(problemDetails);
            
            return;
        }

        var result = await orders.CreateOrder.Execute(userId, request);
        
        if (result.Errors.Any())
        {
            var errorsMap = result.Errors.Select(e => new { e.ErrorCode, e.Message });
            
            var problemDetails = new ProblemDetails
            {
                Title = "Errors found",
                Detail = "Error creating the order",
                Status = 400,
                Extensions = { { "Errors", errorsMap } }
            };
            
            await Clients.User(Context.ConnectionId).ReceiveError(problemDetails);
            return;
        }
        
        
        // Send the new order to all users in the "ManagersAndCaptains" group and the user who created it
        await Clients.Group("ManagersAndCaptains").ReceiveOrders(result.Value);
        
        // Send the new order to the user who created it
        await Clients.User(Context.ConnectionId).ReceiveOrders(result.Value);
    }
}