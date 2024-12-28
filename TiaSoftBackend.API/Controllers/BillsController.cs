using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP.APIExtensions;
using TiaSoftBackend.DTOs.Bills;
using TiaSoftBackend.UseCases.Bills;

namespace TiaSoftBackend.API.Controllers;

[ApiController]
[Route("api/bills")]
public class BillsController (BillsUseCases bills): ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan, Mesero")]
    public async Task<IActionResult> CreateBill([FromBody] CreateBillRequest request)
        => await bills.CreateBill.Execute(request)
            .ToValueOrProblemDetails();
    
    [HttpPut("{billId:required}")]
    [Authorize(Roles = "SuperUsuario, Gerente, Capitan, Mesero")]
    public async Task<IActionResult> UpdateBill([FromBody] UpdateBillRequest request, [FromRoute] string billId)
        => await bills.UpdateBill.Execute(billId, request)
            .ToValueOrProblemDetails();
}