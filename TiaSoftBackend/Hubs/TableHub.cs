using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TiaSoftBackend.Constants;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Models.Table;
using TiaSoftBackend.Services;

namespace TiaSoftBackend.Hubs;

public interface ITableHub
{
    Task ReceiveTable(TableResponseDto tableResponse);
}

[Authorize]
public class TableHub: Hub<ITableHub>
{
    private readonly ITablesRepository _tablesRepository;
    private readonly ITableStatusesRepository _tableStatusesRepository;
    private readonly IMapper _mapper;
    
    public TableHub(ITablesRepository tablesRepository, ITableStatusesRepository tableStatusesRepository, IMapper mapper)
    {
        _tablesRepository = tablesRepository;
        _tableStatusesRepository = tableStatusesRepository;
        _mapper = mapper;
    }
    
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

    public async Task CreateTable(CreateTableDto table)
    {
        var activeStatus = await _tableStatusesRepository.GetTableStatusByName(TableStatusConstants.Activo.ToString());
        
        var newTable = new TableEntity()
        {
            TableId = Guid.NewGuid().ToString(),
            UserId = Context.UserIdentifier,
            Name = table.Name,
            Customers = table.Customers,
            AreaId = table.AreaId,
            TableStatusId = activeStatus.TableStatusId
        };
        
        var tableEntity = await _tablesRepository.CreateTable(newTable);
        var tableResponse = _mapper.Map<TableResponseDto>(tableEntity);
        
        // Send the new table to all users in the "ManagersAndCaptains" group
        await Clients.Group("ManagersAndCaptains").ReceiveTable(tableResponse);
        
        // Send the new table to the user who created it
        await Clients.User(Context.ConnectionId).ReceiveTable(tableResponse);
    }

    public async Task UpdateTable(string tableId, UpdateTableDto updateTableDto)
    {
        var table = await _tablesRepository.GetTableById(tableId);
        
        table.Name = updateTableDto.Name;
        table.Customers = updateTableDto.Customers;
        table.AreaId = updateTableDto.AreaId;
        
        var result = await _tablesRepository.UpdateTable(table);
        
        var tableResponse = _mapper.Map<TableResponseDto>(result);
        
        // Send the updated table to all users in the "ManagersAndCaptains" and the user who updated it
        await Clients.Group("ManagersAndCaptains").ReceiveTable(tableResponse);
        await Clients.User(Context.ConnectionId).ReceiveTable(tableResponse);
    }

    public async Task SendTableToCashier(string tableId)
    {
        var table = await _tablesRepository.GetTableById(tableId);
        var billStatus = await _tableStatusesRepository.GetTableStatusByName(TableStatusConstants.PorAutorizar.ToString());
        
        table.TableStatusId = billStatus.TableStatusId;
        
        var result = await _tablesRepository.UpdateTable(table);
        
        var tableResponse = _mapper.Map<TableResponseDto>(result);
        
        // Send the updated table to
        // all users in the "ManagersAndCaptains" group and the user who generated the bill
        await Clients.Group("ManagersAndCaptains").ReceiveTable(tableResponse);
        await Clients.User(Context.ConnectionId).ReceiveTable(tableResponse);
    }
}