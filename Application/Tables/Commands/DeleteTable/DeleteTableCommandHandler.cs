using Application.Common.Interfaces;
using MediatR;

namespace Application.Tables.Commands.DeleteTable;

public class DeleteTableCommandHandler(ITableRepository tableRepository)
    : IRequestHandler<DeleteTableCommand>
{
    public async Task Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        await tableRepository.DeleteAsync(request.Id, cancellationToken);
    }
}