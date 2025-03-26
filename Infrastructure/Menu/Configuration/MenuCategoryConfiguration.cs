using Domain.Entities.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Menu.Configuration;

public class MenuCategoryConfiguration : IEntityTypeConfiguration<MenuCategory>
{
    public void Configure(EntityTypeBuilder<MenuCategory> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .UseIdentityColumn();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(x => x.IsAvailable)
            .IsRequired()
            .HasDefaultValue(true);
            
        builder.HasMany(x => x.Positions)
            .WithOne(x => x.MenuCategory)
            .HasForeignKey(x => x.MenuCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 