using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiaSoftBackend.Data.Entities;

public class Product
{
    [Key]
    [MaxLength(36)]
    public string ProductId { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; }
    
    [MaxLength(100)]
    public string? Description { get; set; }
    
    [MaxLength(100)]
    public decimal Price { get; set; }
    
    public bool IsAvailable { get; set; }
    
    [MaxLength(255)]
    public string? ImageUrl { get; set; }
    
    [ForeignKey("CategoryId")]
    [MaxLength(36)]
    public string CategoryId { get; set; }
    public Category Category { get; set; }
    
    // M-M With OrderProduct
    public ICollection<OrderProduct> Orders { get; set; }
}