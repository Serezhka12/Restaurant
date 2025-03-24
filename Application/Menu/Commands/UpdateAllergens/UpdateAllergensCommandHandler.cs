using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.UpdateAllergens;

public class UpdateAllergensCommandHandler : IRequestHandler<UpdateAllergensCommand>
{
    private readonly IAllergensRepository _allergensRepository;

    public UpdateAllergensCommandHandler(IAllergensRepository allergensRepository)
    {
        _allergensRepository = allergensRepository;
    }

    public async Task Handle(UpdateAllergensCommand request, CancellationToken cancellationToken)
    {
        var allergen = await _allergensRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (allergen == null)
        {
            throw new NotFoundException($"Алерген з ID {request.Id} не знайдений");
        }

        allergen.Name = request.Name;

        await _allergensRepository.UpdateAsync(allergen, cancellationToken);
    }
} 