using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.DeleteAllergens;

public record DeleteAllergensCommand(int Id) : IRequest;

public class DeleteAllergensCommandHandler(
    IAllergensRepository allergensRepository,
    IApplicationDbContext dbContext)
    : IRequestHandler<DeleteAllergensCommand>
{
    public async Task Handle(DeleteAllergensCommand request, CancellationToken cancellationToken)
    {
        var exists = await allergensRepository.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
        {
            throw new NotFoundException($"Алерген з ID {request.Id} не знайдений");
        }

        await allergensRepository.DeleteAsync(request.Id, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 