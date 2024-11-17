using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models.Order;
    
public partial class ProductItemDto
{
    [Required]
    public string ProductId { get; set; }
        
    [Required]
    public int Quantity { get; set; }
}

public partial class BillDto
{
    [Required]
    public string BillId { get; set; }
    
    [Required]
    public string TableId { get; set; }
    
    [Required]
    public Decimal Total { get; set; }
    
    [Required]
    public List<ProductItemDto> Items { get; set; }
}

public class CreateOrderDto
{
    public string? AdditionalInfo { get; set; }
    
    [DataType(DataType.Text)]
    public string? CustomerName { get; set; }
    
    [Required]
    public List<BillDto> Bills { get; set; }
}