using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Tables;

public class UpdateTableRequest
{
    [Required]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    public required string AreaId { get; set; }
}