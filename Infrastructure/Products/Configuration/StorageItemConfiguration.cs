using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Products.Configuration;

public class StorageItemConfiguration : IEntityTypeConfiguration<StorageItem>
{
    public void Configure(EntityTypeBuilder<StorageItem> builder)
    {
        builder.HasKey(si => si.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn();

        builder.Property(si => si.Quantity)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(si => si.ReceivedDate)
            .IsRequired();

        builder.Property(si => si.ExpiryDate)
            .IsRequired();
    }
}