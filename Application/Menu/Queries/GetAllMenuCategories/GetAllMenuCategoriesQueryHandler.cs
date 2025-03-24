using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Mapster;
using MediatR;

namespace Application.Menu.Queries.GetAllMenuCategories;

public class GetAllMenuCategoriesQueryHandler : IRequestHandler<GetAllMenuCategoriesQuery, List<MenuCategoryDto>>
{
    private readonly IMenuCategoryRepository _menuCategoryRepository;

    public GetAllMenuCategoriesQueryHandler(IMenuCategoryRepository menuCategoryRepository)
    {
        _menuCategoryRepository = menuCategoryRepository;
    }

    public async Task<List<MenuCategoryDto>> Handle(GetAllMenuCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _menuCategoryRepository.GetAllAsync(cancellationToken);
        return categories.Adapt<List<MenuCategoryDto>>();
    }
} 