using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend.Services;

public interface ITablesRepository
{
    Task<IEnumerable<TableEntity>> GetTables();
    Task<TableEntity> CreateTable(TableEntity table);
    Task<TableEntity> UpdateTable(TableEntity table);
    Task<TableEntity> GetTableById(string tableId);
}

public class TablesRepository: ITablesRepository
{
    private readonly ApplicationDbContext _context;
    public TablesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<TableEntity>> GetTables()
    {
        return await _context.Tables
            .Include(t => t.TableStatus)
            .Include(t => t.User)
            .Include(t => t.Area).ToListAsync();
    }
    
    public async Task<TableEntity> CreateTable(TableEntity table)
    {
        var result = await _context.Tables.AddAsync(table);
        await _context.SaveChangesAsync();

        // Include all navigation properties
        var entity = await _context.Tables
            .Include(t => t.TableStatus)
            .Include(t => t.User)
            .Include(t => t.Area)
            .FirstOrDefaultAsync(t => t.TableId == result.Entity.TableId);
        
        return entity;
    }
    
    public async Task<TableEntity> UpdateTable(TableEntity table)
    {
        var result = _context.Tables.Update(table);
        await _context.SaveChangesAsync();
        
        // Include all navigation properties
        var entity = await _context.Tables
            .Include(t => t.TableStatus)
            .Include(t => t.User)
            .Include(t => t.Area)
            .FirstOrDefaultAsync(t => t.TableId == result.Entity.TableId);
        
        return entity;
    }
    
    public async Task<TableEntity> GetTableById(string tableId)
    {
        return await _context.Tables.FirstOrDefaultAsync(t => t.TableId == tableId);
    }
}