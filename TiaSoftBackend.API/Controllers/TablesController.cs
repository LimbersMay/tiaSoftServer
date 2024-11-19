using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP;
using ROP.APIExtensions;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Specifications;
using TiaSoftBackend.Data.Specifications.TableSpecs;
using TiaSoftBackend.DTOs.Tables;
using TiaSoftBackend.UseCases.ErrorCodes;
using TiaSoftBackend.UseCases.Tables;

namespace TiaSoftBackend.API.Controllers;

[ApiController]
[Route("api/tables")]
public class TablesController (TablesUseCases tables) : ControllerBase
{
    
    [HttpGet("all")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetTables()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Result.NotFound<TableDto>(ErrorCodes.UserNotFound.ToString())
                .ToValueOrProblemDetails();
        }

        return await tables.GetTables.Execute(new UserIdSpecification(userId))
            .ToValueOrProblemDetails();
    }


    [HttpGet("{tableId}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan, Mesero")]
    public async Task<IActionResult> GetTableById(string tableId)
        => await tables.GetTable.Execute(new TableIdSpecification(tableId))
            .ToValueOrProblemDetails();
    
    [HttpGet("activeAndNotPaid")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetActiveAndNotPaidTables()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userId is null)
        {
            return Result.NotFound<TableDto>(ErrorCodes.UserNotFound.ToString())
                .ToValueOrProblemDetails();
        }
        
        var userIdAndNotPaidTableSpec = 
            new AndSpecification<TableEntity>(
            new UserIdSpecification(userId), 
            new PaidTableSpecification().Not());
        
        /* Filter by:
         1. User Id
         2. Status != Paid
        */

        return await tables
            .GetTables.Execute(userIdAndNotPaidTableSpec)
            .ToValueOrProblemDetails();
    }
    
    [HttpGet("statuses")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetTableStatuses()
        => await tables.GetTableStatuses.Execute()
            .ToValueOrProblemDetails();
    
    [HttpGet("tableExists")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> TableExists(string tableName)
    {
        
        var tableNameAndNotPaidTableSpec = new AndSpecification<TableEntity>(
            new TableNameSpecification(tableName),
            new PaidTableSpecification().Not());
        
        /*
         * An active table is a table with a status of "Activo" or "PorAutorizar".
         * An active table is also a table that has not been paid.
         */

        return await tables.GetTable.Execute(tableNameAndNotPaidTableSpec)
            .ToValueOrProblemDetails();
    }
    
    // Update table and create table are located in the table Hub
}