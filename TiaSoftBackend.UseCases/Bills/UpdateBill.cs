using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.Data.Specifications.BillSpecs;
using TiaSoftBackend.DTOs.Bills;

namespace TiaSoftBackend.UseCases.Bills;

public class UpdateBill (IBillsRepository billsRepository, IMapper mapper)
{
    public async Task<Result<BillDto>> Execute(string billId, UpdateBillRequest request)
    {
        var billToUpdate = await billsRepository.GetBill(new BillIdSpecification(billId));
        
        if (billToUpdate is null)
            return Result.NotFound<BillDto>(ErrorCodes.ErrorCodes.BillNotFound);

        billToUpdate.Name = request.Name;
        
        var result = await billsRepository.UpdateBill(billToUpdate);
        
        return mapper.Map<BillDto>(result);
    }
}