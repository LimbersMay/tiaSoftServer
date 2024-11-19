using Microsoft.EntityFrameworkCore;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Specifications;

namespace TiaSoftBackend.Data.Repositories;

public interface IBillsRepository
{
    Task<Bill> CreateBill(Bill bill);
    Task<Bill?> GetBill(Specification<Bill> specification);
    Task<Bill> GetBillById(string bill);
    Task<Bill> UpdateBill(Bill bill);
}

public class BillsRepository: IBillsRepository
{
    private readonly ApplicationDbContext _context;
    
    public BillsRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Bill> CreateBill(Bill bill)
    {
        var result = await _context.Bills.AddAsync(bill);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }
    
    public async Task<Bill> GetBillById(string billId)
    {
        return await _context.Bills.FindAsync(billId);
    }
    
    public async Task<Bill?> GetBill(Specification<Bill> specification)
    {
        return await _context.Bills.FirstOrDefaultAsync(specification.ToExpression());
    }
    
    public async Task<Bill> UpdateBill(Bill bill)
    {
        var result = _context.Bills.Update(bill);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }
}