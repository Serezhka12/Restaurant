using Application.Common.Interfaces;
using MediatR;

namespace Application.Staff.Commands.DeleteStaff;

public class DeleteStaffCommandHandler(IEmployeeRepository employeeRepository)
    : IRequestHandler<DeleteStaffCommand>
{
    public async Task Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
    {
        await employeeRepository.DeleteAsync(request.Id, cancellationToken);
    }
} 