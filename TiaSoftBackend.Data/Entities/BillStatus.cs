using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Data.Entities;

public class BillStatus
{
    [Key]
    [MaxLength(36)]
    public required string BillStatusId { get; set; }
    
    [MaxLength(36)]
    public required string Status { get; set; }
    
    [MaxLength(100)]
    public required string Description { get; set; }
}