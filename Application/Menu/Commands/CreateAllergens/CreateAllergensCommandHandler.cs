using Application.Common.Interfaces;
using Domain.Entities.Menu;
using MediatR;

namespace Application.Menu.Commands.CreateAllergens;

public record CreateAllergensCommand(string Name) : IRequest<int>;

public class CreateAllergensCommandHandler(
    IAllergensRepository allergensRepository,
    IApplicationDbContext dbContext)
    : IRequestHandler<CreateAllergensCommand, int>
{
    public async Task<int> Handle(CreateAllergensCommand request, CancellationToken cancellationToken)
    {
        var allergen = new Allergens
        {
            Name = request.Name,
            Positions = []
        };

        var id = await allergensRepository.AddAsync(allergen, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return id;
    }
}