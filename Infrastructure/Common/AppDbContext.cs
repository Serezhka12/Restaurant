using Application.Common.Interfaces;
using Domain.Entities.Products;
using Domain.Entities.Reservation;
using Domain.Entities.Staff;
using Infrastructure.Products.Configuration;
using Infrastructure.Reservation.Configuration;
using Infrastructure.Staff.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<StorageItem> StorageItems { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<EmployeeWorkDay> WorkDays { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<TableReservation> TableReservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new StorageItemConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeWorkDayConfiguration());
        modelBuilder.ApplyConfiguration(new TableConfiguration());
        modelBuilder.ApplyConfiguration(new TableReservationConfiguration());
    }
}