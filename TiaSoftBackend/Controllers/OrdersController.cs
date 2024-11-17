using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Constants;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Models.Order;
using TiaSoftBackend.Services;
using TiaSoftBackend.Specifications.OrderSpecs;

namespace TiaSoftBackend.controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController: ControllerBase
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMenuRepository _menuRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    
    public OrdersController(
        IOrdersRepository ordersRepository, 
        UserManager<User> userManager,
        IMenuRepository menuRepository,
        IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _userManager = userManager;
        _menuRepository = menuRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan, Mesero")]
    public async Task<IActionResult> GetOrders()
    {
        var userId = _userManager.GetUserId(User);

        var orders = await _ordersRepository.
            GetOrdersBySpecification(new UserIdSpecification(userId));
        
        var ordersResponse = _mapper.Map<List<OrderResponseDto>>(orders);
        
        return new JsonResult(ordersResponse);
    }
    
    [HttpPost("{areaId:required}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan, Mesero")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orders, [FromRoute] string areaId)
    {
        var userId = _userManager.GetUserId(User);
        var defaultOrderStatus = await _ordersRepository.
            GetOrderBySpecification(new ActiveOrderSpecification());
        
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
            
            var newOrder = new Order
            {
                OrderId = newOrderId,
                UserId = userId,
                TableId = bill.TableId,
                AreaId = areaId,
                OrderStatusId = defaultOrderStatus.OrderStatusId,
                BillId = bill.BillId,
                TotalPrice = billTotal
            };
            
            newOrder.Products = products;
            ordersToPrint.Add(newOrder);
            
            await _ordersRepository.CreateOrder(newOrder);
        }
        
        return new JsonResult(ordersToPrint);
    }
}