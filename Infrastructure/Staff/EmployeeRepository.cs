using Application.Common.Interfaces;
using Domain.Entities.Staff;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Staff;

public class EmployeeRepository(AppDbContext context) :  IEmployeeRepository
{

    public async Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Employee
            .Include(s => s.EmployeeWorkDays)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Employee
            .Include(s => s.EmployeeWorkDays)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await context.Employee.AddAsync(employee, cancellationToken);
        return employee.Id;
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        context.Employee.Update(employee);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await context.Employee.FindAsync([id], cancellationToken);
        if (employee != null)
        {
            context.Employee.Remove(employee);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Employee.AnyAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.Employee.AnyAsync(s => s.Name == name, cancellationToken);
    }
}