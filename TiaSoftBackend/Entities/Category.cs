using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Entities;

public class Category
{
    [Key]
    [Required]
    [DataType(DataType.Text)]
    public string CategoryId { get; set; }
    
    [Required]
    [MaxLength(36)]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [MaxLength(255)]
    public string? Description { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Icon { get; set; }
}