namespace Application.Products.Contracts;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalQuantity { get; set; }
    public decimal AvailableQuantity { get; set; }
    public bool IsLowOnStock { get; set; }

    public List<StorageItemDto> StorageItems { get; set; }
}