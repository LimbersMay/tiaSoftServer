using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TiaSoftBackend.Data.Entities;

public class TableEntity
{
    [MaxLength(100)]
    [Key]
    public string TableId { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    // Navigation properties
    [ForeignKey("UserId")]
    [MaxLength(100)]
    public string UserId { get; set; }
    public User User { get; set; }
    
    // N-M With area
    // A table can be in one area and an area can have many tables
    [ForeignKey("AreaId")]
    [MaxLength(100)]
    public string AreaId { get; set; }
    public Area Area { get; set; }
    
    // N-M With TableStatus
    // A table can have one status and a status can be assigned to many tables
    [ForeignKey("TableStatusId")]
    [MaxLength(100)]
    public string TableStatusId { get; set; }
    public TableStatus TableStatus { get; set; }
    
    [ForeignKey("UserId")]
    [MaxLength(100)]
    public string? PaymentAuthorizedByUserId { get; set; }
    public User? PaymentAuthorizedByUser { get; set; }
    
    // M-N With Bill
    // A table can have many bills, and a bill can only be assigned to one table
    public ICollection<Bill> Bills { get; set; }
}