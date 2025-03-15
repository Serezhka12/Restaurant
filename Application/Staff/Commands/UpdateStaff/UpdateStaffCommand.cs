using Domain.Entities.Staff;
using MediatR;

namespace Application.Staff.Commands.UpdateStaff;

public record UpdateStaffCommand(
    int Id,
    string Name,
    Roles Role,
    int Salary,
    List<DayOfWeek> WorkDays) : IRequest;