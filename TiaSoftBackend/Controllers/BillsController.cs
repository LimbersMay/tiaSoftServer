using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiaSoftBackend.Entities;
using TiaSoftBackend.Models;
using TiaSoftBackend.Services;

namespace TiaSoftBackend.controllers;

[ApiController]
[Route("api/bills")]
public class BillsController: ControllerBase
{
    private readonly IBillsRepository _billsRepository;
    
    public BillsController(IBillsRepository billsRepository)
    {
        _billsRepository = billsRepository;
    }
    
    /*
     * POST /api/bills
     * When the user creates a bill, the system needs basic information like the name of the bill, the table id, and the total amount.
     */
    
    [HttpPost]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan, Mesero")]
    public async Task<IActionResult> CreateBill([FromBody] CreateBillDto bill)
    {
        var newBill = new Bill
        {
            Name = bill.Name,
            TableId = bill.TableId,
            Total = 0,
            BillId = Guid.NewGuid().ToString()
        };
        
        var result = await _billsRepository.CreateBill(newBill);
        
        return new JsonResult(result);
    }
    
    [HttpPut("{billId:required}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan, Mesero")]
    public async Task<IActionResult> UpdateBill([FromBody] UpdateBillDto bill, [FromRoute] string billId)
    {
        var billToUpdate = await _billsRepository.GetBillById(billId);
        
        billToUpdate.Name = bill.Name;
        
        var result = await _billsRepository.UpdateBill(billToUpdate);
        
        return new JsonResult(result);
    }
}