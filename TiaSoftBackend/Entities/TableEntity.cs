using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiaSoftBackend.Entities;

public class TableEntity
{
    [MaxLength(100)]
    [Key]
    public string TableId { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    public int Customers { get; set; }
    
    // Navigation properties
    [ForeignKey("UserId")]
    [MaxLength(100)]
    public string UserId { get; set; }
    public User User { get; set; }
    
    [ForeignKey("AreaId")]
    [MaxLength(100)]
    public string AreaId { get; set; }
    public Area Area { get; set; }
    
    [ForeignKey("TableStatusId")]
    [MaxLength(100)]
    public string TableStatusId { get; set; }
    public TableStatus TableStatus { get; set; }
}