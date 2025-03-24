using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.UpdateMenuCategory;

public class UpdateMenuCategoryCommandHandler : IRequestHandler<UpdateMenuCategoryCommand>
{
    private readonly IMenuCategoryRepository _menuCategoryRepository;

    public UpdateMenuCategoryCommandHandler(IMenuCategoryRepository menuCategoryRepository)
    {
        _menuCategoryRepository = menuCategoryRepository;
    }

    public async Task Handle(UpdateMenuCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _menuCategoryRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (category == null)
        {
            throw new NotFoundException($"Категорія меню з ID {request.Id} не знайдена");
        }

        category.Name = request.Name;
        category.IsAvailable = request.IsAvailable;

        await _menuCategoryRepository.UpdateAsync(category, cancellationToken);
    }
} 