using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Entities;

public class DailyOrderCounter
{
    [Key]
    [MaxLength(100)]
    public string DailyOrderCounterId { get; set; }
    public DateTime OrderDate { get; set; }
    public int OrderCount { get; set; }
}