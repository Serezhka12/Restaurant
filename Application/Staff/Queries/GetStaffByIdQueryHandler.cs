using Application.Common.Interfaces;
using Application.Staff.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace Application.Staff.Queries.GetStaffById;

public record GetStaffByIdQuery(int Id) : IRequest<StaffDto>;

public class GetStaffByIdQueryHandler(IEmployeeRepository employeeRepository, IApplicationDbContext dbContext)
    : IRequestHandler<GetStaffByIdQuery, StaffDto>
{
    public async Task<StaffDto> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
    {
        var staff = await dbContext.Employee
            .Include(e => e.EmployeeWorkDays)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (staff == null)
        {
            throw new NotFoundException($"Співробітник з ID {request.Id} не знайдений");
        }

        return new StaffDto
        {
            Id = staff.Id,
            Name = staff.Name,
            Role = staff.Role,
            Salary = staff.Salary,
            WorkDays = staff.EmployeeWorkDays.Select(wd => wd.WorkDay).ToList()
        };
    }
} 