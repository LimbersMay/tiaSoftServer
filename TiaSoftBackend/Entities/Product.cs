using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiaSoftBackend.Entities;

public class Product
{
    [Key]
    [Required]
    public string ProductId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public bool IsAvailable { get; set; }
    
    [Required]
    public string? ImageUrl { get; set; }
    
    [ForeignKey("CategoryId")]
    [Required]
    public string CategoryId { get; set; }
    public Category Category { get; set; }
}