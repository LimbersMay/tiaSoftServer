using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend.Services;

public interface IMenuRepository
{
    Task<IEnumerable<Product>> GetMenu();
    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(Product product);
}

public class MenuRepository: IMenuRepository
{
    private ApplicationDbContext _dbContext;
    
    public MenuRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Product>> GetMenu()
    {
        var products = await _dbContext.Products
            .Include(p => p.Category)
            .ToListAsync();
        
        return products;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        var result = await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var result = _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
        
        // Load the Category navigation property
        await _dbContext.Entry(result.Entity).Reference(p => p.Category).LoadAsync();
        
        return result.Entity;
    }
}