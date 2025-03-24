using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.DeleteMenuPosition;

public class DeleteMenuPositionCommandHandler : IRequestHandler<DeleteMenuPositionCommand>
{
    private readonly IMenuPositionRepository _menuPositionRepository;

    public DeleteMenuPositionCommandHandler(IMenuPositionRepository menuPositionRepository)
    {
        _menuPositionRepository = menuPositionRepository;
    }

    public async Task Handle(DeleteMenuPositionCommand request, CancellationToken cancellationToken)
    {
        var exists = await _menuPositionRepository.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
        {
            throw new NotFoundException($"Позиція меню з ID {request.Id} не знайдена");
        }

        await _menuPositionRepository.DeleteAsync(request.Id, cancellationToken);
    }
} 