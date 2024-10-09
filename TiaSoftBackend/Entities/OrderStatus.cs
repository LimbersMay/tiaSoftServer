using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Entities;

public class OrderStatus
{
    [Key]
    [MaxLength(36)]
    public string OrderStatusId { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; }
    
    [MaxLength(100)]
    public string Description { get; set; }
}