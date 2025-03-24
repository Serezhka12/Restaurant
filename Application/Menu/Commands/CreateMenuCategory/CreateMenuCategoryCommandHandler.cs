using Application.Common.Interfaces;
using Domain.Entities.Menu;
using MediatR;

namespace Application.Menu.Commands.CreateMenuCategory;

public class CreateMenuCategoryCommandHandler : IRequestHandler<CreateMenuCategoryCommand, int>
{
    private readonly IMenuCategoryRepository _menuCategoryRepository;

    public CreateMenuCategoryCommandHandler(IMenuCategoryRepository menuCategoryRepository)
    {
        _menuCategoryRepository = menuCategoryRepository;
    }

    public async Task<int> Handle(CreateMenuCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new MenuCategory
        {
            Name = request.Name,
            IsAvailable = request.IsAvailable,
            Positions = new List<MenuPosition>()
        };

        return await _menuCategoryRepository.AddAsync(category, cancellationToken);
    }
} 