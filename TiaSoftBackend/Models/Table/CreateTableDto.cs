using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models.Table;

public class CreateTableDto
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [Required]
    [Range(1, 100)]
    public int Customers { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    public string AreaId { get; set; }
}