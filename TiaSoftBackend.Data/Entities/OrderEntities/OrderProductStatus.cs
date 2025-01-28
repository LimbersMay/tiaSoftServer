using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Data.Entities.OrderEntities;

public class OrderProductStatus
{
    [Key]
    [MaxLength(36)]
    public required string OrderProductStatusId { get; set; }
    
    [MaxLength(50)]
    public required string Name { get; set; }
    
    [MaxLength(50)]
    public required string Value { get; set; }
    
    [MaxLength(200)]
    public required string Description { get; set; }
}