namespace TiaSoftBackend.DTOs.Menu;

public class MenuDto
{
    public required string ProductId { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required bool IsAvailable { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required string Category { get; set; }
    public required string CategoryId { get; set; }
}