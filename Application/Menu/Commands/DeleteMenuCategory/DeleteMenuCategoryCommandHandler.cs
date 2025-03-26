using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.DeleteMenuCategory;

public record DeleteMenuCategoryCommand(int Id) : IRequest;

public class DeleteMenuCategoryCommandHandler(
    IMenuCategoryRepository menuCategoryRepository,
    IApplicationDbContext dbContext)
    : IRequestHandler<DeleteMenuCategoryCommand>
{
    public async Task Handle(DeleteMenuCategoryCommand request, CancellationToken cancellationToken)
    {
        var exists = await menuCategoryRepository.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
        {
            throw new NotFoundException($"Категорія меню з ID {request.Id} не знайдена");
        }

        await menuCategoryRepository.DeleteAsync(request.Id, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 