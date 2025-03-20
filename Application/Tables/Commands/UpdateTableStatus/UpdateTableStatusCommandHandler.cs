using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Tables.Commands.UpdateTableStatus;

public class UpdateTableStatusCommandHandler(ITableRepository tableRepository)
    : IRequestHandler<UpdateTableStatusCommand>
{
    public async Task Handle(UpdateTableStatusCommand request, CancellationToken cancellationToken)
    {
        var table = await tableRepository.GetByIdAsync(request.Id, cancellationToken);

        if (table == null)
        {
            throw new NotFoundException($"Table with ID {request.Id} not found");
        }

        table.IsFree = request.IsFree;

        await tableRepository.UpdateAsync(table, cancellationToken);
    }
}