namespace Domain.Entities.Products;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal MinimumQuantity { get; set; }
    public List<StorageItem> StorageItems { get; set; } = new List<StorageItem>();
}