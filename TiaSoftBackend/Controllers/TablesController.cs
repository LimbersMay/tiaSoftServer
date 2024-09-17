using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Enums;
using TiaSoftBackend.Models.Table;
using TiaSoftBackend.Services;

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
    
    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan")]
    public async Task<IActionResult> GetTables()
    {
        var tables = await _tablesRepository.GetTables();

        Console.WriteLine(Guid.NewGuid().ToString());

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
        var table = await _tablesRepository.GetActiveTable(tableName);
        
        if (table is null)
        {
            return NotFound(ErrorCodes.TableNotFound.ToString());
        }
        
        return new JsonResult(_mapper.Map<TableResponseDto>(table));
    }
    
    // Update table and create table are located in the table Hub
}