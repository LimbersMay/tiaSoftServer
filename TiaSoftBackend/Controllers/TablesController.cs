using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Enums;
using TiaSoftBackend.Models.Table;
using TiaSoftBackend.Services;
using TiaSoftBackend.Specifications;
using TiaSoftBackend.Specifications.TableSpecs;

namespace TiaSoftBackend.controllers;

[ApiController]
[Route("api/tables")]
public class TablesController: ControllerBase
{
    private readonly ITablesRepository _tablesRepository;
    private readonly ITableStatusesRepository _tableStatusesRepository;
    private readonly IMapper _mapper;
    
    public TablesController(
        ITablesRepository tablesRepository, 
        ITableStatusesRepository tableStatusesRepository,
        IMapper mapper)
    {
        _tablesRepository = tablesRepository;
        _tableStatusesRepository = tableStatusesRepository;
        _mapper = mapper;
    }
    
    [HttpGet("all")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetTables()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var tables = await _tablesRepository.GetTables(new UserIdSpecification(userId));

        var tablesResponse = _mapper.Map<List<TableResponseDto>>(tables);
        
        return new JsonResult(tablesResponse);
    }
    
        
    [HttpGet("{tableId}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan, Mesero")]
    public async Task<IActionResult> GetTableById(string tableId)
    {
        var table = await _tablesRepository.GetTable(new TableIdSpecification(tableId));
        
        if (table is null)
        {
            return NotFound(ErrorCodes.TableNotFound.ToString());
        }
        
        return new JsonResult(_mapper.Map<TableResponseDto>(table));
    }
    
    [HttpGet("activeAndNotPaid")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetActiveAndNotPaidTables()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var userIdSpec = new UserIdSpecification(userId);
        var notPaidTableSpec = new PaidTableSpecification().Not();
        
        /* Filter by:
         1. User Id
         2. Status != Paid
        */
        
        var tables = await _tablesRepository
            .GetTables(userIdSpec.And(notPaidTableSpec));
        
        var tablesResponse = _mapper.Map<List<TableResponseDto>>(tables);
        
        return new JsonResult(tablesResponse);
    }
    
    [HttpGet("statuses")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetTableStatuses()
    {
        var tableStatuses = await _tableStatusesRepository.GetTableStatuses();
        return new JsonResult(tableStatuses);
    }
    
    [HttpGet("tableExists")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> TableExists(string tableName)
    {
        var tableNameSpec = new TableNameSpecification(tableName);
        var notPaidTableSpec = new PaidTableSpecification().Not();
        
        /*
         * An active table is a table with a status of "Activo" or "PorAutorizar".
         * An active table is also a table that has not been paid.
         */
        
        var table = await _tablesRepository.GetTable(tableNameSpec.And(notPaidTableSpec));
        
        if (table is null)
        {
            return NotFound(ErrorCodes.TableNotFound.ToString());
        }
        
        return new JsonResult(_mapper.Map<TableResponseDto>(table));
    }
    
    // Update table and create table are located in the table Hub
}