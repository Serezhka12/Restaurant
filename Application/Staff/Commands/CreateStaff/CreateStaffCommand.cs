using Domain.Entities.Staff;
using MediatR;

namespace Application.Staff.Commands.CreateStaff;

public record CreateStaffCommand(
    string Name,
    Roles Role,
    int Salary,
    List<DayOfWeek> WorkDays) : IRequest<int>;