using TiaSoftBackend.Models.Area;

namespace TiaSoftBackend.Models.Table;

public class TableResponseDto
{
    public string TableId { get; set; }
    public string Name { get; set; }
    public int Customers { get; set; }
    public UserResponseDto User { get; set; }
    public AreaResponseDto Area { get; set; }
    public TableStatusResponseDto TableStatus { get; set; }
}