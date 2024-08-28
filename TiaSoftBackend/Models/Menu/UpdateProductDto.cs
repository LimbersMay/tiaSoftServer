namespace TiaSoftBackend.Models.Product;

public class UpdateProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string CategoryId { get; set; }
}