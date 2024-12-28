using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Areas;

public class CreateAreaRequest
{
    [Required]
    public required string Name { get; set; }
    public required string Description { get; set; }
}