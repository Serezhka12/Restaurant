using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Menu.Commands.UpdateMenuCategory;

public class UpdateMenuCategoryCommandHandler : IRequestHandler<UpdateMenuCategoryCommand>
{
    private readonly IMenuCategoryRepository _menuCategoryRepository;
    private readonly IMenuPositionRepository _menuPositionRepository;

    public UpdateMenuCategoryCommandHandler(
        IMenuCategoryRepository menuCategoryRepository,
        IMenuPositionRepository menuPositionRepository)
    {
        _menuCategoryRepository = menuCategoryRepository;
        _menuPositionRepository = menuPositionRepository;
    }

    public async Task Handle(UpdateMenuCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _menuCategoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException($"Menu category with ID {request.Id} not found");
        }

        var isAvailabilityChanged = category.IsAvailable && !request.IsAvailable;

        category.Name = request.Name;
        category.IsAvailable = request.IsAvailable;

        await _menuCategoryRepository.UpdateAsync(category, cancellationToken);

        if (isAvailabilityChanged)
        {
            var positions = await _menuPositionRepository.GetAllByCategoryIdAsync(request.Id, cancellationToken);
            foreach (var position in positions)
            {
                position.IsAvailable = false;
                await _menuPositionRepository.UpdateAsync(position, cancellationToken);
            }
        }
    }
}