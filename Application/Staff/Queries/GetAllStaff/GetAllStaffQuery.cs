using Application.Staff.Contracts;
using MediatR;

namespace Application.Staff.Queries.GetAllStaff;

public record GetAllStaffQuery : IRequest<List<StaffDto>>; 