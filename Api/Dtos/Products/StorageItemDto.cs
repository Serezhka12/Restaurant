namespace Shared.Dtos.Products;

public class StorageItemDto
{
    public int Id { get; set; }
    public decimal Quantity { get; set; }
    public DateTime ReceivedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal PurchasePrice { get; set; }
    public bool IsExpired { get; set; }
} 