using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Bills;

namespace TiaSoftBackend.UseCases.Bills;

public class CreateBill (IBillsRepository billsRepository, IMapper mapper)
{
    public async Task<Result<BillDto>> Execute(CreateBillRequest request, string userId)
    {
        var bill = new Bill
        {
            BillId = Guid.NewGuid().ToString(),
            Name = request.Name,
            TableId = request.TableId,
            Total = 0,
        };
        
        var result = await billsRepository.CreateBill(bill);
        
        return mapper.Map<BillDto>(result);
    }
}