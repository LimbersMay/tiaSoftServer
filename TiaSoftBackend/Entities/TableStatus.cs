using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TiaSoftBackend.Entities;

public class TableStatus
{
    [Key]
    [MaxLength(100)]
    public string TableStatusId { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(100)]
    public string Description { get; set; }
    
    // Navigation properties
    [JsonIgnore]
    public List<TableEntity> Tables { get; set; }
}