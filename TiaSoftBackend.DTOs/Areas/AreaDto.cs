namespace TiaSoftBackend.DTOs.Areas;

public class AreaDto
{
    public required string AreaId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}