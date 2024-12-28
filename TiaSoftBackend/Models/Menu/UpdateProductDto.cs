using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models.Product;

public class UpdateProductDto
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public bool IsAvailable { get; set; }
    
    public string ImageUrl { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string CategoryId { get; set; }
}