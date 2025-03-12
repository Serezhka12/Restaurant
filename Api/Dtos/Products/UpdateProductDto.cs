namespace Shared.Dtos.Products;

public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal MinimumQuantity { get; set; }
} 