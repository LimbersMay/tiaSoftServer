namespace TiaSoftBackend.DTOs.Orders;

public class OrderStatusDto
{
    public required string OrderStatusId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
}