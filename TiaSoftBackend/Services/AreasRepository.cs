using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend.Services;

public interface IAreasRepository
{
    Task<IEnumerable<Area>> GetAreas();
    Task<Area> CreateArea(Area area);
    Task<Area> UpdateArea(Area area);
}

public class AreasRepository: IAreasRepository
{
    private readonly ApplicationDbContext _context;
    
    public AreasRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }
    
    public async Task<IEnumerable<Area>> GetAreas()
    {
        return await _context.Areas.ToListAsync();
    }
    
    public async Task<Area> CreateArea(Area area)
    {
        var result = await _context.Areas.AddAsync(area);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }
    
    public async Task<Area> UpdateArea(Area area)
    {
        var result = _context.Areas.Update(area);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }
}