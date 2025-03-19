using Domain.Entities.Products;
using Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<StorageItem> StorageItems { get; set; }
    DbSet<Employee> Employee { get; set; }
    DbSet<EmployeeWorkDay> WorkDays { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}