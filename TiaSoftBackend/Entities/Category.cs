using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Entities;

public class Category
{
    [Key]
    [Required]
    [DataType(DataType.Text)]
    public string CategoryId { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    public string? Description { get; set; }
}