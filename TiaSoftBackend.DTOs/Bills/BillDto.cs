namespace TiaSoftBackend.DTOs.Bills;

public class BillDto
{
    public required string TableId { get; set; }
    public required string BillId { get; set; }
    public required string Name { get; set; }
    public required decimal Total { get; set; }
}