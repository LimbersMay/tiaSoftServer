using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TiaSoftBackend.Constants;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Models.Order;
using TiaSoftBackend.Services;
using TiaSoftBackend.Specifications.OrderSpecs;

namespace TiaSoftBackend.Hubs;

public interface IOrderHub
{
    Task ReceiveOrders(List<OrderResponseDto> orders);
}

[Authorize]
public class OrderHub: Hub<IOrderHub>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;
    private readonly IMenuRepository _menuRepository;
    
    public OrderHub(
        IOrdersRepository ordersRepository, 
        IMapper mapper,
        IMenuRepository menuRepository)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
        _menuRepository = menuRepository;
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
    
    public async Task CreateOrder(CreateOrderDto orders, string areaId)
    {
        var userId = Context.UserIdentifier;
        var defaultOrderStatus = await _ordersRepository
            .GetOrderBySpecification(new ActiveOrderSpecification());
        
        var ordersToPrint = new List<Order>();

        foreach (var bill in orders.Bills)
        {
            var newOrderId = Guid.NewGuid().ToString();
            
            /*
             * Create a list of OrderProduct objects to store the menu products in the order
             */
            var products = new List<OrderProduct>();
            var billTotal = 0.0m;
            
            foreach (var item in bill.Items)
            {
                var product = await _menuRepository.GetProductById(item.ProductId);
                
                // Calculate the total price of the bill by adding the price of each product multiplied by the quantity
                billTotal += product.Price * item.Quantity;
    
                products.Add(new OrderProduct
                {
                    OrderId = newOrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PriceAtOrder = product.Price,
                });
            }
            
            // Calculate the orderId, current date without time, only dd/MM/yyyy
            var orderCounter = await _ordersRepository.GetOrdersCount(DateTime.Today);
            
            var newOrder = new Order
            {
                OrderId = newOrderId,
                UserId = userId,
                TableId = bill.TableId,
                AreaId = areaId,
                OrderStatusId = defaultOrderStatus.OrderStatusId,
                BillId = bill.BillId,
                TotalPrice = billTotal,
                OrderNumber = orderCounter
            };
            
            newOrder.Products = products;
            ordersToPrint.Add(newOrder);
            
            await _ordersRepository.CreateOrder(newOrder);
        }
        
        var ordersToPrintDto = _mapper.Map<List<OrderResponseDto>>(ordersToPrint);
        
        // Send the new order to all users in the "ManagersAndCaptains" group and the user who created it
        await Clients.Group("ManagersAndCaptains").ReceiveOrders(ordersToPrintDto);
        
        // Send the new order to the user who created it
        await Clients.User(Context.ConnectionId).ReceiveOrders(ordersToPrintDto);
    }
}