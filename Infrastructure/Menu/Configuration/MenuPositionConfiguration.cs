using Domain.Entities.Menu;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Menu.Configuration;

public class MenuPositionConfiguration : IEntityTypeConfiguration<MenuPosition>
{
    public void Configure(EntityTypeBuilder<MenuPosition> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.IsVegan)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.IsAvailable)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.MenuCategoryId)
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasMany(x => x.Products)
            .WithMany(x => x.MenuPositions)
            .UsingEntity<Dictionary<string, object>>(
                "MenuPositionProducts",
                j => j
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<MenuPosition>()
                    .WithMany()
                    .HasForeignKey("MenuPositionId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("MenuPositionId", "ProductId");
                    j.ToTable("MenuPositionProducts");
                });
    }
}