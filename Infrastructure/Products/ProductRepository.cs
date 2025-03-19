using Application.Common.Interfaces;
using Domain.Entities.Products;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Products;

public class ProductRepository(AppDbContext context) : RepositoryBase(context), IProductRepository
{
    private readonly AppDbContext _context = context;

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
    public async Task<int> AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return product.Id;
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FindAsync([id], cancellationToken);
        if (product != null)
        {
            _context.Products.Remove(product);
            await SaveChangesAsync(cancellationToken);
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
}