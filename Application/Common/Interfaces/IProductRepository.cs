using Domain.Entities.Products;

namespace Application.Common.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Product>> GetLowOnStockAsync(CancellationToken cancellationToken = default);
    Task<int> AddAsync(Product product, CancellationToken cancellationToken = default);
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    Task RemoveExpiredItemsAsync(CancellationToken cancellationToken = default);
    Task AddToStorageAsync(Product product, decimal quantity, DateTime expiryDate, decimal purchasePrice , CancellationToken cancellationToken = default);
    Task UseProductAsync(Product product, decimal quantity, CancellationToken cancellationToken = default);


    // Методи для роботи з бізнес-логікою
    decimal GetTotalQuantity(Product product);
    decimal GetAvailableQuantity(Product product);
    bool IsLowOnStock(Product product);
    bool IsExpired(StorageItem storageItem);
}