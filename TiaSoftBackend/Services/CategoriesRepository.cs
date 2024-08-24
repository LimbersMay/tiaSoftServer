using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend.Services;

public interface ICategoriesRepository
{
    Task<IEnumerable<Category>> GetCategories();
    Task<Category> CreateCategory(Category category);
    Task<Category> UpdateCategory(Category category);   
}

public class CategoriesRepository: ICategoriesRepository
{
    private readonly ApplicationDbContext _context;
    
    public CategoriesRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> CreateCategory(Category category)
    {
        var result = await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<Category> UpdateCategory(Category category)
    {
        var result = _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return result.Entity;
    }
}