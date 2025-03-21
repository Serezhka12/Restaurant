using Domain.Entities.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Reservation.Configuration;

public class TableConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Seats)
            .IsRequired();

        builder.Property(t => t.IsFree)
            .IsRequired();

    }
}