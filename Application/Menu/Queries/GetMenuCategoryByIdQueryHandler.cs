using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Mapster;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Queries.GetMenuCategoryById;

public record GetMenuCategoryByIdQuery(int Id) : IRequest<MenuCategoryDto>;

public class GetMenuCategoryByIdQueryHandler(IMenuCategoryRepository menuCategoryRepository)
    : IRequestHandler<GetMenuCategoryByIdQuery, MenuCategoryDto>
{
    public async Task<MenuCategoryDto> Handle(GetMenuCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await menuCategoryRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (category == null)
        {
            throw new NotFoundException($"Категорія меню з ID {request.Id} не знайдена");
        }
        
        return category.Adapt<MenuCategoryDto>();
    }
} 