using TiaSoftBackend.Entities;

namespace TiaSoftBackend.Models.Order;

public class OrderResponseDto
{
    public string OrderId { get; set; }
    public string TableName { get; set; }
    public int OrderNumber { get; set; }
    public Decimal TotalPrice { get; set; }
    public UserResponseDto User { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<OrderProductResponse> Products { get; set; }
}