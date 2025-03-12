using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<StorageItem> StorageItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}