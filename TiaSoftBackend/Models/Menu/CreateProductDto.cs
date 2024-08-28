using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models.Menu;

public class CreateProductDto
{ 
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public bool IsAvailable { get; set; }
    
    public string? ImageUrl { get; set; }
    
    [Required]
    public string CategoryId { get; set; }
}