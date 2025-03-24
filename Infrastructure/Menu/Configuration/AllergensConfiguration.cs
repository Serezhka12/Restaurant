using Domain.Entities.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Menu.Configuration;

public class AllergensConfiguration : IEntityTypeConfiguration<Allergens>
{
    public void Configure(EntityTypeBuilder<Allergens> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .UseIdentityColumn();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        // Створення проміжної таблиці для зв'язку багато-до-багатьох з MenuPosition
        builder.HasMany(x => x.Positions)
            .WithMany(x => x.Allergens)
            .UsingEntity<Dictionary<string, object>>(
                "MenuPositionAllergens",
                j => j
                    .HasOne<MenuPosition>()
                    .WithMany()
                    .HasForeignKey("MenuPositionId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Allergens>()
                    .WithMany()
                    .HasForeignKey("AllergenId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("MenuPositionId", "AllergenId");
                    j.ToTable("MenuPositionAllergens");
                });
    }
} 