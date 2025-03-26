using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Products.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.Unit)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.MinimumQuantity)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.HasMany(p => p.StorageItems)
            .WithOne()
            .HasForeignKey("ProductId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}