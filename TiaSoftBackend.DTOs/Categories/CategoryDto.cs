using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Categories;

public class CategoryDto
{
    public required string Name { get; set; }
    
    public string? Description { get; set; }
    
    public required string Icon { get; set; }
    
    public required string CategoryId { get; set; }
}