using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Categories;

public class UpdateCategoryRequest
{
    [Required]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
    
    [DataType(DataType.Text)]
    public string? Description { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    public required string Icon { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    public required string CategoryId { get; set; }
}