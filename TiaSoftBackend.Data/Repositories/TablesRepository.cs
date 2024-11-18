using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Specifications;

namespace TiaSoftBackend.Data.Repositories;

public interface ITablesRepository
{
    Task<List<TableEntity>> GetTables(Specification<TableEntity> specification);
    Task<TableEntity> GetTable(Specification<TableEntity> specification);
    Task<TableEntity> CreateTable(TableEntity table);
    Task<TableEntity> UpdateTable(TableEntity table);
}

public class TablesRepository : ITablesRepository
{
    private readonly ApplicationDbContext _context;

    public TablesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<TableEntity>> GetTables(Specification<TableEntity> specification)
    {
        return await _context.Tables
            .Include(t => t.TableStatus)
            .Include(t => t.User)
            .Include(t => t.Area)
            .Include(t => t.PaymentAuthorizedByUser)
            .Where(specification.ToExpression())
            .ToListAsync();
    }
    
    public async Task<TableEntity> GetTable(Specification<TableEntity> specification)
    {
        return await _context.Tables
            .Include(t => t.TableStatus)
            .Include(t => t.User)
            .Include(t => t.Area)
            .Include(t => t.PaymentAuthorizedByUser)
            .FirstOrDefaultAsync(specification.ToExpression());
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
            .Include(t => t.PaymentAuthorizedByUser)
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
            .Include(t => t.PaymentAuthorizedByUser)
            .FirstOrDefaultAsync(t => t.TableId == result.Entity.TableId);

        return entity;
    }
}