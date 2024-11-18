using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TiaSoftBackend.Data.Entities;

public class Order
{
    [Key]
    [MaxLength(36)]
    public string OrderId { get; set; }
    
    public int OrderNumber { get; set; }
    
    [MaxLength(36)]
    public string AdditionalInfo { get; set; } 
    
    [Required]
    public Decimal TotalPrice { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // FK to User   
    [ForeignKey("UserId")]
    [MaxLength(36)]
    public string UserId { get; set; }
    public User User { get; set; }
    
    // FK to Table
    [ForeignKey("TableId")]
    [MaxLength(36)]
    public string TableId { get; set; }
    public TableEntity Table { get; set; }
    
    // FK to OrderStatus
    [ForeignKey("OrderStatusId")]
    [MaxLength(36)]
    public string OrderStatusId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    
    // FK to Area
    [ForeignKey("AreaId")]
    [MaxLength(36)]
    public string AreaId { get; set; }
    public Area Area { get; set; }
    
    // FK to Bill
    [ForeignKey("BillId")]
    [MaxLength(36)]
    public string BillId { get; set; }
    
    // No navigation property for Bill
    // No need because we can get all orders in a bill
    
    // M-M With OrderProduct
    public ICollection<OrderProduct> Products { get; set; }
}