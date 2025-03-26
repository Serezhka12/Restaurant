using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Mapster;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Queries.GetMenuPositionById;

public record GetMenuPositionByIdQuery(int Id) : IRequest<MenuPositionDto>;

public class GetMenuPositionByIdQueryHandler(IMenuPositionRepository menuPositionRepository)
    : IRequestHandler<GetMenuPositionByIdQuery, MenuPositionDto>
{
    public async Task<MenuPositionDto> Handle(GetMenuPositionByIdQuery request, CancellationToken cancellationToken)
    {
        var position = await menuPositionRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (position == null)
        {
            throw new NotFoundException($"Позиція меню з ID {request.Id} не знайдена");
        }
        
        var result = position.Adapt<MenuPositionDto>();
        
        // Встановлюємо списки ID для алергенів та продуктів
        result.AllergenIds = position.Allergens.Select(a => a.Id).ToList();
        result.ProductIds = position.Products.Select(p => p.Id).ToList();
        
        return result;
    }
} 