using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Menu;

public class UpdateMenuRequest
{
    [Required]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public bool IsAvailable { get; set; }
    
    public required string ImageUrl { get; set; }
    
    [Required]
    public required string Description { get; set; }
    
    [Required]
    public required string CategoryId { get; set; }
}