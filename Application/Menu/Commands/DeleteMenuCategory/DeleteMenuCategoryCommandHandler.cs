using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.DeleteMenuCategory;

public class DeleteMenuCategoryCommandHandler : IRequestHandler<DeleteMenuCategoryCommand>
{
    private readonly IMenuCategoryRepository _menuCategoryRepository;

    public DeleteMenuCategoryCommandHandler(IMenuCategoryRepository menuCategoryRepository)
    {
        _menuCategoryRepository = menuCategoryRepository;
    }

    public async Task Handle(DeleteMenuCategoryCommand request, CancellationToken cancellationToken)
    {
        var exists = await _menuCategoryRepository.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
        {
            throw new NotFoundException($"Категорія меню з ID {request.Id} не знайдена");
        }

        await _menuCategoryRepository.DeleteAsync(request.Id, cancellationToken);
    }
} 