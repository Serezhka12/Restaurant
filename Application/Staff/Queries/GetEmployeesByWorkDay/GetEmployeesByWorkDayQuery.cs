using Application.Staff.Contracts;
using Domain.Entities.Staff;
using MediatR;

namespace Application.Staff.Queries.GetEmployeesByWorkDay;

public record GetEmployeesByWorkDayQuery(DayOfWeek Day) : IRequest<List<StaffDto>>;