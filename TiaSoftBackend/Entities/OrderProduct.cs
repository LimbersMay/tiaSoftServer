using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Entities;

public class OrderProduct
{
    // The orderId and productId are used as a composite key
    [MaxLength(36)]
    public string OrderId { get; set; }
    public Order Order { get; set; }
    
    [MaxLength(36)]
    public string ProductId { get; set; }
    public Product Product { get; set; }
    
    // Quantity of the product in the order
    public int Quantity { get; set; }
    
    // Price of the product at the time of the order
    public decimal PriceAtOrder { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}