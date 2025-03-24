using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.DeleteAllergens;

public class DeleteAllergensCommandHandler : IRequestHandler<DeleteAllergensCommand>
{
    private readonly IAllergensRepository _allergensRepository;

    public DeleteAllergensCommandHandler(IAllergensRepository allergensRepository)
    {
        _allergensRepository = allergensRepository;
    }

    public async Task Handle(DeleteAllergensCommand request, CancellationToken cancellationToken)
    {
        var exists = await _allergensRepository.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
        {
            throw new NotFoundException($"Алерген з ID {request.Id} не знайдений");
        }

        await _allergensRepository.DeleteAsync(request.Id, cancellationToken);
    }
} 