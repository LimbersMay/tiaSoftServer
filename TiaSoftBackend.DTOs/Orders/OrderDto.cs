using TiaSoftBackend.DTOs.Users;

namespace TiaSoftBackend.DTOs.Orders;

public class OrderDto
{
    public required string OrderId { get; set; }
    public required string TableName { get; set; }
    public int OrderNumber { get; set; }
    public Decimal TotalPrice { get; set; }
    public required UserDto User { get; set; }
    public required OrderStatusDto OrderStatus { get; set; }
    public required List<OrderProductDto> Products { get; set; }
}