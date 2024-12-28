using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Specifications;

namespace TiaSoftBackend.Services;

public interface IOrdersRepository
{
    /// <summary>
    /// Get all orders based on a specification
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    Task<IEnumerable<Order>> GetOrdersBySpecification(Specification<Order> specification);
    
    /// <summary>
    /// Get an order based on a specification
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    Task<Order> GetOrderBySpecification(Specification<Order> specification);
    
    Task<Order> CreateOrder(Order order);
    
    /// <summary>
    /// Get the number of orders for a given date
    /// </summary>
    /// <param name="orderDate"> The date to get the orders count for</param>
    /// <returns></returns>
    Task<int> GetOrdersCount(DateTime orderDate);
}

public class OrdersRepository: IOrdersRepository
{
    private ApplicationDbContext _dbContext;
    
    public OrdersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> GetOrderBySpecification(Specification<Order> specification)
    {
        var order = await _dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Table)
            .Include(o => o.OrderStatus)
            .Include(o => o.Area)
            .Include(o => o.Products)
            .FirstOrDefaultAsync(specification.ToExpression());
        
        return order;
    }
    
    public async Task<IEnumerable<Order>> GetOrdersBySpecification(Specification<Order> specification)
    {
        var orders = await _dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Table)
            .Include(o => o.OrderStatus)
            .Include(o => o.Area)
            .Include(o => o.Products)
            .Where(specification.ToExpression())
            .ToListAsync();
        
        return orders;
    }
    
    public async Task<IEnumerable<Order>> GetOrders()
    {
        var orders = await _dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Table)
            .Include(o => o.OrderStatus)
            .Include(o => o.Area)
            .Include(o => o.Products)
            .ToListAsync();
        
        return orders;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        var result = await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        
        return result.Entity;
    }
    
    /// <summary>
    /// Get the number of orders for a given date
    /// </summary>
    /// <param name="orderDate"> The date to get the orders count for</param>
    /// <returns></returns>
    public async Task<int> GetOrdersCount(DateTime orderDate)
    {
        var today = DateTime.Today;
     
        // Check if there's a DailyOrderCounter for today
        var orderCounter = await _dbContext.DailyOrderCounters.FirstOrDefaultAsync(oc =>oc.OrderDate == today);
        
        if (orderCounter == null)
        {
            orderCounter = new DailyOrderCounter
            {
                DailyOrderCounterId = Guid.NewGuid().ToString(),
                OrderDate = today,
                OrderCount = 1
            };
            
            await _dbContext.DailyOrderCounters.AddAsync(orderCounter);
        }
        else
        {
            orderCounter.OrderCount++;
        }
        
        await _dbContext.SaveChangesAsync();
        return orderCounter.OrderCount;
    }
}