using TiaSoftBackend.Models.Product;

namespace TiaSoftBackend.Models.Order;

public class OrderProductResponse
{
    public string OrderId { get; set; }
    public string ProductId { get; set; }
    
    public ProductResponseDto Product { get; set; }
    public int Quantity { get; set; }
    
    public decimal PriceAtOrder { get; set; }
}