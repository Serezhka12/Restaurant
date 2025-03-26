using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Mapster;
using MediatR;

namespace Application.Menu.Queries.GetAllMenuCategories;

public record GetAllMenuCategoriesQuery : IRequest<List<MenuCategoryDto>>;

public class GetAllMenuCategoriesQueryHandler(IMenuCategoryRepository menuCategoryRepository)
    : IRequestHandler<GetAllMenuCategoriesQuery, List<MenuCategoryDto>>
{
    public async Task<List<MenuCategoryDto>> Handle(GetAllMenuCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await menuCategoryRepository.GetAllAsync(cancellationToken);
        return categories.Adapt<List<MenuCategoryDto>>();
    }
} 