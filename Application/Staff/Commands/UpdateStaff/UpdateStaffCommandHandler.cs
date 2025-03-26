using Application.Common.Interfaces;
using Domain.Entities.Staff;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace Application.Staff.Commands.UpdateStaff;

public record UpdateStaffCommand(
    int Id,
    string Name,
    Roles Role,
    int Salary,
    List<DayOfWeek> WorkDays) : IRequest;

public class UpdateStaffCommandHandler(IEmployeeRepository employeeRepository, IApplicationDbContext dbContext)
    : IRequestHandler<UpdateStaffCommand>
{
    public async Task Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
    {
        var staff = await employeeRepository.GetByIdAsync(request.Id, cancellationToken);

        if (staff == null)
        {
            throw new NotFoundException($"Співробітник з ID {request.Id} не знайдений");
        }

        staff.Name = request.Name;
        staff.Role = request.Role;
        staff.Salary = request.Salary;


        staff.EmployeeWorkDays.Clear();
        foreach (var day in request.WorkDays)
        {
            staff.EmployeeWorkDays.Add(new EmployeeWorkDay { EmployeeId = staff.Id, WorkDay = day });
        }

        await employeeRepository.UpdateAsync(staff, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}