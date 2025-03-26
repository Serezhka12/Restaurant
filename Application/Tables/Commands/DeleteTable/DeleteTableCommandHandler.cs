using Application.Common.Interfaces;
using MediatR;

namespace Application.Tables.Commands.DeleteTable;

public record DeleteTableCommand(int Id) : IRequest;

public class DeleteTableCommandHandler(ITableRepository tableRepository, IApplicationDbContext dbContext)
    : IRequestHandler<DeleteTableCommand>
{
    public async Task Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        await tableRepository.DeleteAsync(request.Id, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}