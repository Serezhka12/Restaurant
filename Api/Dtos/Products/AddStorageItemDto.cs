namespace Shared.Dtos.Products;

public class AddStorageItemDto
{
    public decimal Quantity { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal PurchasePrice { get; set; }
} 