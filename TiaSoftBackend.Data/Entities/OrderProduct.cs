using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TiaSoftBackend.Data.Entities.OrderEntities;

namespace TiaSoftBackend.Data.Entities;

public class OrderProduct
{
    // The orderId and productId are used as a composite key
    [MaxLength(36)]
    public required string OrderId { get; set; }
    public Order Order { get; set; }
    
    [MaxLength(36)]
    public required string ProductId { get; set; }
    public Product Product { get; set; }
    
    // Foreign key to OrderProductStatus
    [ForeignKey("OrderProductStatusId")]
    [MaxLength(36)]
    // TODO: Mark as required
    public string OrderProductStatusId { get; set; }
    public OrderProductStatus OrderProductStatus { get; set; }
    
    // Quantity of the product in the order
    public int Quantity { get; set; }
    
    // Price of the product at the time of the order
    public required decimal PriceAtOrder { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}