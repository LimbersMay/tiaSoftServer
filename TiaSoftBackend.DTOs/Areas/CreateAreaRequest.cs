namespace TiaSoftBackend.DTOs.Areas;

public class CreateAreaRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}