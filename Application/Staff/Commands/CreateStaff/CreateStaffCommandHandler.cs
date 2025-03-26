using Application.Common.Interfaces;
using Domain.Entities.Staff;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Staff.Commands.CreateStaff;

public record CreateStaffCommand(
    string Name,
    Roles Role,
    int Salary,
    List<DayOfWeek> WorkDays) : IRequest<int>;

public class CreateStaffCommandHandler(IEmployeeRepository employeeRepository, IApplicationDbContext dbContext)
    : IRequestHandler<CreateStaffCommand, int>
{
    public async Task<int> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
    {
        var staff = new Employee
        {
            Name = request.Name,
            Role = request.Role,
            Salary = request.Salary,
            EmployeeWorkDays = request.WorkDays.Select(day => new EmployeeWorkDay { WorkDay = day }).ToList()
        };

        var id = await employeeRepository.AddAsync(staff, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return id;
    }
}