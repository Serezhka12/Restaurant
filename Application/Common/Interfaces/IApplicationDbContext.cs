using Domain.Entities.Menu;
using Domain.Entities.Products;
using Domain.Entities.Reservation;
using Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<StorageItem> StorageItems { get; set; }
    DbSet<Employee> Employee { get; set; }
    DbSet<EmployeeWorkDay> WorkDays { get; set; }
    DbSet<Table> Tables { get; set; }
    DbSet<TableReservation> TableReservations { get; set; }
    DbSet<MenuCategory> MenuCategories { get; set; }
    DbSet<Allergens> Allergens { get; set; }
    DbSet<MenuPosition> MenuPositions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}