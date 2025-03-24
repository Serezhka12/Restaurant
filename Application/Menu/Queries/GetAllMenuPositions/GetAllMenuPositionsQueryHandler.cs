using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Mapster;
using MediatR;

namespace Application.Menu.Queries.GetAllMenuPositions;

public class GetAllMenuPositionsQueryHandler : IRequestHandler<GetAllMenuPositionsQuery, List<MenuPositionDto>>
{
    private readonly IMenuPositionRepository _menuPositionRepository;

    public GetAllMenuPositionsQueryHandler(IMenuPositionRepository menuPositionRepository)
    {
        _menuPositionRepository = menuPositionRepository;
    }

    public async Task<List<MenuPositionDto>> Handle(GetAllMenuPositionsQuery request, CancellationToken cancellationToken)
    {
        var positions = await _menuPositionRepository.GetAllAsync(cancellationToken);
        var result = positions.Adapt<List<MenuPositionDto>>();
        
        // Встановлюємо списки ID для алергенів та продуктів
        for (int i = 0; i < positions.Count; i++)
        {
            result[i].AllergenIds = positions[i].Allergens.Select(a => a.Id).ToList();
            result[i].ProductIds = positions[i].Products.Select(p => p.Id).ToList();
        }
        
        return result;
    }
} 