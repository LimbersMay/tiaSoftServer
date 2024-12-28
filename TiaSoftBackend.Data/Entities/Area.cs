using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Data.Entities;

public class Area
{
    public string AreaId { get; set; }
    
    [MaxLength(36)]
    public required string Name { get; set; }
    
    [MaxLength(100)]
    public string? Description { get; set; }
}