using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Data.Entities;

public class Category
{
    [Key]
    [Required]
    [DataType(DataType.Text)]
    public required string CategoryId { get; set; }
    
    [Required]
    [MaxLength(36)]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
    
    [MaxLength(255)]
    public string? Description { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string Icon { get; set; }
}