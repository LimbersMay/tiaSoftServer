using TiaSoftBackend.Data.Entities;

namespace TiaSoftBackend.Data.Repositories;

public interface IOrderProductRepository
{
    Task<OrderProduct> UpdateOrderProduct(OrderProduct orderProduct);
}

public class OrderProductRepository : IOrderProductRepository
{
    private ApplicationDbContext _dbContext;

    public OrderProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderProduct> UpdateOrderProduct(OrderProduct orderProduct)
    {
        var result = _dbContext.OrderProducts.Update(orderProduct);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }
}