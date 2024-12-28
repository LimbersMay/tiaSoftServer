using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Bills;

public class CreateBillRequest
{
    [Required]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
    
    [Required]
    public required string TableId { get; set; }
}