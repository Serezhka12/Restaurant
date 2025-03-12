namespace Domain.Entities.Products;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal MinimumQuantity { get; set; }
    public List<StorageItem> StorageItems { get; set; } = new List<StorageItem>();

    // Перенесені методи бізнес-логіки
    public decimal GetTotalQuantity()
    {
        return StorageItems.Sum(si => si.Quantity);
    }

    public decimal GetAvailableQuantity()
    {
        return StorageItems
            .Where(si => !si.IsExpired())
            .Sum(si => si.Quantity);
    }

    public bool IsLowOnStock()
    {
        return GetAvailableQuantity() < MinimumQuantity;
    }
}