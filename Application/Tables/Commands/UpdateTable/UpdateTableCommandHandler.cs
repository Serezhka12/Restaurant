using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Tables.Commands.UpdateTable;

public record UpdateTableCommand(
    int Id,
    int Seats) : IRequest;

public class UpdateTableCommandHandler(ITableRepository tableRepository, IApplicationDbContext dbContext)
    : IRequestHandler<UpdateTableCommand>
{
    public async Task Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var table = await tableRepository.GetByIdAsync(request.Id, cancellationToken);

        if (table == null)
        {
            throw new NotFoundException($"Table with ID {request.Id} not found");
        }

        table.Seats = request.Seats;

        await tableRepository.UpdateAsync(table, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}