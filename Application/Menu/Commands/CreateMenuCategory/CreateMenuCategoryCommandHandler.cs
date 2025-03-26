using Application.Common.Interfaces;
using Domain.Entities.Menu;
using MediatR;

namespace Application.Menu.Commands.CreateMenuCategory;

public record CreateMenuCategoryCommand(
    string Name,
    bool IsAvailable) : IRequest<int>;

public class CreateMenuCategoryCommandHandler(
    IMenuCategoryRepository menuCategoryRepository,
    IApplicationDbContext dbContext)
    : IRequestHandler<CreateMenuCategoryCommand, int>
{
    public async Task<int> Handle(CreateMenuCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new MenuCategory
        {
            Name = request.Name,
            IsAvailable = request.IsAvailable,
            Positions = []
        };

        var id = await menuCategoryRepository.AddAsync(category, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return id;
    }
}