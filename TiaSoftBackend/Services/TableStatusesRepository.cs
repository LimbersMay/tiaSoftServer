using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend.Services;

public interface ITableStatusesRepository
{
    Task<IEnumerable<TableStatus>> GetTableStatuses();
    Task<TableStatus> GetTableStatusByName(string name);
}

public class TableStatusesRepository: ITableStatusesRepository
{
    private readonly ApplicationDbContext _context;
    
    public TableStatusesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<TableStatus>> GetTableStatuses()
    {
        return await _context.TableStatuses.ToListAsync();
    }
    
    public async Task<TableStatus> GetTableStatusByName(string name)
    {
        return await _context.TableStatuses.FirstOrDefaultAsync(ts => ts.Name == name);
    }
}