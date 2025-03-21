using Domain.Entities.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Reservation.Configuration;

public class TableReservationConfiguration : IEntityTypeConfiguration<TableReservation>
{
    public void Configure(EntityTypeBuilder<TableReservation> builder)
    {
        builder.HasKey(tr => tr.Id);

        builder.Property(tr => tr.ReservationDate)
            .IsRequired();

        builder.Property(tr => tr.NumberOfPeople)
            .IsRequired();

        builder.HasOne(tr => tr.Table)
            .WithMany(t => t.Reservations)
            .HasForeignKey(tr => tr.TableId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(tr => tr.ReservationDate);

        builder.HasIndex(tr => new { tr.TableId, tr.ReservationDate }).IsUnique();
    }
}