using Application.Staff.Contracts;
using MediatR;

namespace Application.Staff.Queries.GetStaffById;

public record GetStaffByIdQuery(int Id) : IRequest<StaffDto>; 