using Application.Common.Interfaces;
using Domain.Entities.Staff;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Staff;

public class EmployeeRepository(AppDbContext context) : RepositoryBase(context), IEmployeeRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Employee
            .Include(s => s.EmployeeWorkDays)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Employee
            .Include(s => s.EmployeeWorkDays)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await _context.Employee.AddAsync(employee, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return employee.Id;
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _context.Employee.Update(employee);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _context.Employee.FindAsync([id], cancellationToken);
        if (employee != null)
        {
            _context.Employee.Remove(employee);
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Employee.AnyAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Employee.AnyAsync(s => s.Name == name, cancellationToken);
    }
}