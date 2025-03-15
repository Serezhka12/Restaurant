using MediatR;

namespace Application.Staff.Commands.DeleteStaff;

public record DeleteStaffCommand(int Id) : IRequest; 