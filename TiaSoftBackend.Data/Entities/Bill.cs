using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TiaSoftBackend.Data.Entities;

public class Bill
{
    [Key]
    [MaxLength(36)]
    public string BillId { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(36)]
    public decimal Total { get; set; }
    
    // FK to Table
    [ForeignKey("TableId")]
    [MaxLength(36)]
    public string TableId { get; set; }
    
    public TableEntity Table { get; set; }
    
    // Orders in the bill
    public IEnumerable<Order> Orders { get; set; }
}