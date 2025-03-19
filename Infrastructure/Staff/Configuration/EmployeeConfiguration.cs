using Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Staff.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Role)
            .IsRequired();

        builder.Property(s => s.Salary)
            .IsRequired();

        builder.HasIndex(s => s.Name);

        builder.HasMany(e => e.EmployeeWorkDays)
            .WithOne(ewd => ewd.Employee)
            .HasForeignKey(ewd => ewd.EmployeeId);

    }
}