using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models.Table;

public class CreateTableDto
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    public string AreaId { get; set; }
}