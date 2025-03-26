using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.UpdateAllergens;

public record UpdateAllergensCommand(
    int Id,
    string Name) : IRequest;

public class UpdateAllergensCommandHandler(
    IAllergensRepository allergensRepository,
    IApplicationDbContext dbContext)
    : IRequestHandler<UpdateAllergensCommand>
{
    public async Task Handle(UpdateAllergensCommand request, CancellationToken cancellationToken)
    {
        var allergen = await allergensRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (allergen == null)
        {
            throw new NotFoundException($"Алерген з ID {request.Id} не знайдений");
        }

        allergen.Name = request.Name;

        await allergensRepository.UpdateAsync(allergen, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 