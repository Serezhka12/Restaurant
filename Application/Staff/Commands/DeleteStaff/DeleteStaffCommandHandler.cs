using Application.Common.Interfaces;
using MediatR;

namespace Application.Staff.Commands.DeleteStaff;

public record DeleteStaffCommand(int Id) : IRequest;

public class DeleteStaffCommandHandler(IEmployeeRepository employeeRepository, IApplicationDbContext dbContext)
    : IRequestHandler<DeleteStaffCommand>
{
    public async Task Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
    {
        await employeeRepository.DeleteAsync(request.Id, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 