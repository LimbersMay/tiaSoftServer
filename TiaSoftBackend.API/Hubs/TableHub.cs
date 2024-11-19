using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TiaSoftBackend.Data.Constants;
using TiaSoftBackend.Data.Specifications.TableSpecs;
using TiaSoftBackend.DTOs.Bills;
using TiaSoftBackend.DTOs.Tables;
using TiaSoftBackend.UseCases.Bills;
using TiaSoftBackend.UseCases.ErrorCodes;
using TiaSoftBackend.UseCases.Tables;

namespace TiaSoftBackend.API.Hubs;

public interface ITableHub
{
    Task ReceiveTable(TableDto tableDto);
    Task ReceiveError(ProblemDetails problemDetails);
}

[Authorize]
public class TableHub (TablesUseCases tables, BillsUseCases bills) : Hub<ITableHub>
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

    public async Task CreateTable(CreateTableRequest request)
    {
        /*
         * Get the user identifier from the connection context
         * If the user identifier is null, return an error
         */
        var userId = Context.UserIdentifier;
        
        if (userId is null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = ErrorCodes.UserNotFound.ToString(),
                Detail = "User not found",
                Status = 404,
                Extensions =
                {
                    { ErrorCodes.UserNotFound.ToString(), ErrorCodes.UserNotFound.ToString() }
                }
            };
            
            await Clients.User(Context.ConnectionId).ReceiveError(problemDetails);
            
            return;
        }
        
        /*
         * Execute the "CreateTable" use case
         * If there are any errors, return them
         */
        var newTable = await tables.CreateTable.Execute(request, userId);

        if (newTable.Errors.Any())
        {
            var errorsMap = newTable.Errors.Select(e => new { e.ErrorCode, e.Message });
            
            var problemDetails = new ProblemDetails
            {
                Title = "Errors found",
                Detail = "Error creating table",
                Status = 400,
                Extensions = { { "Errors", errorsMap } }
            };
            
            await Clients.User(Context.ConnectionId).ReceiveError(problemDetails);
            
            return;
        }
        
        var bill = new CreateBillRequest
        {
            TableId = newTable.Value.TableId,
            Name = "Cuenta de " + newTable.Value.Name,
        };
        
        /*
         * Execute the "CreateBill" use case
         * If there are any errors, return them
         */
        
        var result = await bills.CreateBill.Execute(bill, userId);
        
        if (result.Errors.Any())
        {
            var errorsMap = result.Errors.Select(e => new { e.ErrorCode, e.Message });
            
            var problemDetails = new ProblemDetails
            {
                Title = "Errors found",
                Detail = "Error creating bill",
                Status = 400,
                Extensions = { { "Errors", errorsMap } }
            };
            
            await Clients.User(Context.ConnectionId).ReceiveError(problemDetails);
            
            return;
        }
        
        // Send the new table to all users in the "ManagersAndCaptains" group
        await Clients.Group("ManagersAndCaptains").ReceiveTable(newTable.Value);
        
        // Send the new table to the user who created it
        await Clients.User(Context.ConnectionId).ReceiveTable(newTable.Value);
    }

    public async Task UpdateTable(string tableId, UpdateTableRequest request)
    {
        /*
         * Execute the "UpdateTable" use case
         * If there are any errors, return them
         */
        var result = await tables.UpdateTable.Execute(tableId, request);
        
        if (result.Errors.Any())
        {
            var errorsMap = result.Errors.Select(e => new { e.ErrorCode, e.Message });
            
            var problemDetails = new ProblemDetails
            {
                Title = "Errors found",
                Detail = "Error updating table",
                Status = 400,
                Extensions = { { "Errors", errorsMap } }
            };
            
            await Clients.User(Context.ConnectionId).ReceiveError(problemDetails);
            
            return;
        }

        Console.WriteLine("Table updated" + result.Value.Name);
        
        // Send the updated table to all users in the "ManagersAndCaptains" and the user who updated it
        await Clients.Group("ManagersAndCaptains").ReceiveTable(result.Value);
        await Clients.User(Context.ConnectionId).ReceiveTable(result.Value);
    }

    public async Task SendTableToCashier(string tableId)
    {
        var result = await tables.SendTableToCashier.Execute(tableId);
        
        if (result.Errors.Any())
        {
            var errorsMap = result.Errors.Select(e => new { e.ErrorCode, e.Message });
            
            var problemDetails = new ProblemDetails
            {
                Title = "Errors found",
                Detail = "Error sending table to cashier",
                Status = 400,
                Extensions = { { "Errors", errorsMap } }
            };
            
            await Clients.User(Context.ConnectionId).ReceiveError(problemDetails);
            
            return;
        }
        
        // Send the updated table to
        // all users in the "ManagersAndCaptains" group and the user who generated the bill
        await Clients.Group("ManagersAndCaptains").ReceiveTable(result.Value);
        await Clients.User(Context.ConnectionId).ReceiveTable(result.Value);
    }
}