using TiaSoftBackend.DTOs.Menu;

namespace TiaSoftBackend.DTOs.Orders;

public class OrderProductDto
{
    public required string OrderId { get; set; }
    public required string ProductId { get; set; }
    
    public required string ProductName { get; set; }
    public int Quantity { get; set; }
    
    public decimal PriceAtOrder { get; set; }
}