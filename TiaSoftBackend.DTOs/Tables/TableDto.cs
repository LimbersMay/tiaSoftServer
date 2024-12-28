using TiaSoftBackend.DTOs.Areas;
using TiaSoftBackend.DTOs.Bills;
using TiaSoftBackend.DTOs.Users;

namespace TiaSoftBackend.DTOs.Tables;

public class TableDto
{
    public required string TableId { get; set; }
    public required string Name { get; set; }
    public required UserDto User { get; set; }
    public required AreaDto Area { get; set; }
    public required TableStatusDto TableStatus { get; set; }
    public required UserDto PaymentAuthorizedByUser { get; set; }
    public required IEnumerable<BillDto> Bills { get; set; }
}