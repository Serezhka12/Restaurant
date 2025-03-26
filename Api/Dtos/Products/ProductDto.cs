namespace Api.Dtos.Products;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal MinimumQuantity { get; set; }
    public decimal TotalQuantity { get; set; }
    public decimal AvailableQuantity { get; set; }
    public bool IsLowOnStock { get; set; }
    public List<StorageItemDto> StorageItems { get; set; } = new List<StorageItemDto>();
}