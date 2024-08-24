using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models.Category;

public class CategoryDtoBase
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    public string? Description { get; set; }
}