using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Orders;

public class ProductItemRequest
{
    [Required]
    public required string ProductId { get; set; }
        
    [Required]
    public int Quantity { get; set; }
}

public class BillRequest
{
    [Required]
    public required string BillId { get; set; }
    
    [Required]
    public required string TableId { get; set; }
    
    [Required]
    public Decimal Total { get; set; }
    
    [Required]
    public required List<ProductItemRequest> Items { get; set; }
}

public class CreateOrderRequest
{
    [DataType(DataType.Text)]
    public string? AdditionalInfo { get; set; }
    
    [DataType(DataType.Text)]
    public string? CustomerName { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    public required string AreaId { get; set; }
    
    [Required]
    public required List<BillRequest> Bills { get; set; }
}