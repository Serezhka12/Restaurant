using Application.Common.Interfaces;
using Domain.Entities.Products;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(p => p.StorageItems)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(p => p.StorageItems)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetLowOnStockAsync(CancellationToken cancellationToken = default)
    {
        var products = await _context.Products
            .Include(p => p.StorageItems)
            .ToListAsync(cancellationToken);

        return products.Where(p => IsLowOnStock(p)).ToList();
    }

    public async Task<int> AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FindAsync(new object[] { id }, cancellationToken);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Products.AnyAsync(p => p.Name == name, cancellationToken);
    }

    public async Task AddToStorageAsync(Product product, decimal quantity, DateTime expiryDate, decimal purchasePrice , CancellationToken cancellationToken = default)
    {
        var storageItem = new StorageItem
        {
            ProductId = product.Id,
            Quantity = quantity,
            ReceivedDate = DateTime.UtcNow,
            ExpiryDate = expiryDate,
            PurchasePrice = purchasePrice
        };

        await _context.StorageItems.AddAsync(storageItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveExpiredItemsAsync(CancellationToken cancellationToken = default)
    {
        var expiredItems = await _context.StorageItems
            .Where(si => si.ExpiryDate < DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        _context.StorageItems.RemoveRange(expiredItems);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UseProductAsync(Product product, decimal quantity, CancellationToken cancellationToken = default)
    {
        var availableItems = product.StorageItems
            .Where(si => !IsExpired(si) && si.Quantity > 0)
            .OrderBy(si => si.ExpiryDate)
            .ToList();

        var remainingQuantity = quantity;

        foreach (var item in availableItems.TakeWhile(_ => remainingQuantity > 0))
        {
            if (item.Quantity <= remainingQuantity)
            {
                remainingQuantity -= item.Quantity;
                item.Quantity = 0;
            }
            else
            {
                item.Quantity -= remainingQuantity;
                remainingQuantity = 0;
            }
        }

        if (remainingQuantity > 0)
        {
            throw new InvalidOperationException($"Insufficient product in storage. Missing {remainingQuantity} {product.Unit}");
        }
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public decimal GetTotalQuantity(Product product)
    {
        return product.StorageItems.Sum(si => si.Quantity);
    }

    public decimal GetAvailableQuantity(Product product)
    {
        return product.StorageItems
            .Where(si => !IsExpired(si))
            .Sum(si => si.Quantity);
    }

    public bool IsLowOnStock(Product product)
    {
        return GetAvailableQuantity(product) < product.MinimumQuantity;
    }

    public bool IsExpired(StorageItem storageItem)
    {
        return storageItem.ExpiryDate < DateTime.UtcNow;
    }
}