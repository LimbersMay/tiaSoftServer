namespace TiaSoftBackend.Models;

public class BillResponseDto
{
    public string TableId { get; set; }
    public string BillId { get; set; }
    public string Name { get; set; }
    public decimal Total { get; set; }
}