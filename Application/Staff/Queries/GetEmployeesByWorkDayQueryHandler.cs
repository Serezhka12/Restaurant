using Application.Common.Interfaces;
using Application.Staff.Contracts;
using Domain.Entities.Staff;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Staff.Queries.GetEmployeesByWorkDay;

public record GetEmployeesByWorkDayQuery(DayOfWeek Day) : IRequest<List<StaffDto>>;
public class GetEmployeesByWorkDayQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetEmployeesByWorkDayQuery, List<StaffDto>>
{
    public async Task<List<StaffDto>> Handle(GetEmployeesByWorkDayQuery request, CancellationToken cancellationToken)
    {
        var employees = await dbContext.Employee
            .Include(e => e.EmployeeWorkDays)
            .Where(e => e.EmployeeWorkDays.Any(wd => wd.WorkDay == request.Day))
            .ToListAsync(cancellationToken);

        return employees.Select(e => new StaffDto
        {
            Id = e.Id,
            Name = e.Name,
            Role = e.Role,
            Salary = e.Salary,
            WorkDays = e.EmployeeWorkDays.Select(wd => wd.WorkDay).ToList()
        }).ToList();
    }
}