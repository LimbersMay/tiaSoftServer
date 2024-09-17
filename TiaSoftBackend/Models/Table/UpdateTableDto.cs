using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models.Table;

public class UpdateTableDto
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public int Customers { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    public string AreaId { get; set; }
}