using Domain.Entities.Products;
using Infrastructure.Products.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<StorageItem> StorageItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new StorageItemConfiguration());
    }
}