using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Menu;

public class CreateMenuRequest
{
    [Required]
    public required string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public bool IsAvailable { get; set; }
    
    [Required]
    public required string CategoryId { get; set; }
}