using Application.Common.Interfaces;
using Application.Staff.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Staff.Queries.GetAllStaff;

public class GetAllStaffQueryHandler(IEmployeeRepository employeeRepository, IApplicationDbContext dbContext)
    : IRequestHandler<GetAllStaffQuery, List<StaffDto>>
{
    public async Task<List<StaffDto>> Handle(GetAllStaffQuery request, CancellationToken cancellationToken)
    {
        var staffList = await dbContext.Employee
            .Include(e => e.EmployeeWorkDays)
            .ToListAsync(cancellationToken);

        return staffList.Select(staff => new StaffDto
        {
            Id = staff.Id,
            Name = staff.Name,
            Role = staff.Role,
            Salary = staff.Salary,
            WorkDays = staff.EmployeeWorkDays.Select(wd => wd.WorkDay).ToList()
        }).ToList();
    }
} 