namespace Domain.Entities.Products;

public class StorageItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public decimal Quantity { get; set; }
    public DateTime ReceivedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal PurchasePrice { get; set; }

    public bool IsExpired()
    {
        return ExpiryDate < DateTime.UtcNow;
    }
}