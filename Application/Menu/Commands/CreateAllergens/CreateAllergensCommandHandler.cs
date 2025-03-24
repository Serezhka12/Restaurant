using Application.Common.Interfaces;
using Domain.Entities.Menu;
using MediatR;

namespace Application.Menu.Commands.CreateAllergens;

public class CreateAllergensCommandHandler : IRequestHandler<CreateAllergensCommand, int>
{
    private readonly IAllergensRepository _allergensRepository;

    public CreateAllergensCommandHandler(IAllergensRepository allergensRepository)
    {
        _allergensRepository = allergensRepository;
    }

    public async Task<int> Handle(CreateAllergensCommand request, CancellationToken cancellationToken)
    {
        var allergen = new Allergens
        {
            Name = request.Name,
            Positions = new List<MenuPosition>()
        };

        return await _allergensRepository.AddAsync(allergen, cancellationToken);
    }
} 