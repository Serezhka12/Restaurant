using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.DeleteMenuPosition;

public record DeleteMenuPositionCommand(int Id) : IRequest;

public class DeleteMenuPositionCommandHandler(
    IMenuPositionRepository menuPositionRepository,
    IApplicationDbContext dbContext)
    : IRequestHandler<DeleteMenuPositionCommand>
{
    public async Task Handle(DeleteMenuPositionCommand request, CancellationToken cancellationToken)
    {
        var exists = await menuPositionRepository.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
        {
            throw new NotFoundException($"Позиція меню з ID {request.Id} не знайдена");
        }

        await menuPositionRepository.DeleteAsync(request.Id, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 