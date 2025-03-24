using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Mapster;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Queries.GetMenuCategoryById;

public class GetMenuCategoryByIdQueryHandler : IRequestHandler<GetMenuCategoryByIdQuery, MenuCategoryDto>
{
    private readonly IMenuCategoryRepository _menuCategoryRepository;

    public GetMenuCategoryByIdQueryHandler(IMenuCategoryRepository menuCategoryRepository)
    {
        _menuCategoryRepository = menuCategoryRepository;
    }

    public async Task<MenuCategoryDto> Handle(GetMenuCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _menuCategoryRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (category == null)
        {
            throw new NotFoundException($"Категорія меню з ID {request.Id} не знайдена");
        }
        
        return category.Adapt<MenuCategoryDto>();
    }
} 