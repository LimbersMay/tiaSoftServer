using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.Data.Specifications;
using TiaSoftBackend.DTOs.Orders;

namespace TiaSoftBackend.UseCases.Orders;

public class GetOrders (IOrdersRepository ordersRepository, IMapper mapper)
{
    public async Task<Result<List<OrderDto>>> Execute(Specification<Order> specification)
    {
        var orders = await ordersRepository.GetOrdersBySpecification(specification);
        
        foreach (var order in orders)
        {
            Console.WriteLine($"------------------------------------ Order {order.OrderId} ------------------------------------");
        }
        
        return mapper.Map<List<OrderDto>>(orders);
    }
}