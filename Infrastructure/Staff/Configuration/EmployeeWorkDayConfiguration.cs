using Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Staff.Configuration;

public class EmployeeWorkDayConfiguration : IEntityTypeConfiguration<EmployeeWorkDay>
{
    public void Configure(EntityTypeBuilder<EmployeeWorkDay> builder)
    {
        builder.ToTable("EmployeeWorkDays");

        builder.HasKey(ewd => new { ewd.EmployeeId, ewd.WorkDay });

        builder.Property(ewd => ewd.WorkDay)
            .HasConversion<string>();
    }
}