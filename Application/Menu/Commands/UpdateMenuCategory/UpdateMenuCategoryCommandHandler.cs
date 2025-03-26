using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Menu.Commands.UpdateMenuCategory;

public record UpdateMenuCategoryCommand(
    int Id,
    string Name,
    bool IsAvailable) : IRequest;

public class UpdateMenuCategoryCommandHandler(
    IMenuCategoryRepository menuCategoryRepository,
    IMenuPositionRepository menuPositionRepository,
    IApplicationDbContext dbContext)
    : IRequestHandler<UpdateMenuCategoryCommand>
{
    public async Task Handle(UpdateMenuCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await menuCategoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException($"Menu category with ID {request.Id} not found");
        }

        var isAvailabilityChanged = category.IsAvailable && !request.IsAvailable;

        category.Name = request.Name;
        category.IsAvailable = request.IsAvailable;

        await menuCategoryRepository.UpdateAsync(category, cancellationToken);

        if (isAvailabilityChanged)
        {
            var positions = await menuPositionRepository.GetAllByCategoryIdAsync(request.Id, cancellationToken);
            foreach (var position in positions)
            {
                position.IsAvailable = false;
                await menuPositionRepository.UpdateAsync(position, cancellationToken);
            }
        }
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}