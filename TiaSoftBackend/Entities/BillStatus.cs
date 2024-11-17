using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Entities;

public class BillStatus
{
    [Key]
    [MaxLength(36)]
    public string BillStatusId { get; set; }
    
    [MaxLength(36)]
    public string Status { get; set; }
    
    [MaxLength(100)]
    public string Description { get; set; }
}