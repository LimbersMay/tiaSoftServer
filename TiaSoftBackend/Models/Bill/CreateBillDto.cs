using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models;

public class CreateBillDto
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [Required]
    public string TableId { get; set; }
}