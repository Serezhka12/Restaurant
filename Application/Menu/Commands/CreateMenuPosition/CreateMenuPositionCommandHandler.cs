using Application.Common.Interfaces;
using Domain.Entities.Menu;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.CreateMenuPosition;

public record CreateMenuPositionCommand(
    string Name,
    bool IsVegan,
    bool IsAvailable,
    List<int> AllergenIds,
    int MenuCategoryId,
    decimal Price,
    List<int> ProductIds) : IRequest<int>;

public class CreateMenuPositionCommandHandler(
    IMenuPositionRepository menuPositionRepository,
    IMenuCategoryRepository menuCategoryRepository,
    IAllergensRepository allergensRepository,
    IProductRepository productRepository,
    IApplicationDbContext dbContext)
    : IRequestHandler<CreateMenuPositionCommand, int>
{
    public async Task<int> Handle(CreateMenuPositionCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await menuCategoryRepository.ExistsAsync(request.MenuCategoryId, cancellationToken);
        if (!categoryExists)
        {
            throw new NotFoundException($"Категорія меню з ID {request.MenuCategoryId} не знайдена");
        }

        var position = new MenuPosition
        {
            Name = request.Name,
            IsVegan = request.IsVegan,
            IsAvailable = request.IsAvailable,
            MenuCategoryId = request.MenuCategoryId,
            Price = request.Price,
            Allergens = [],
            Products = []
        };

        if (request.AllergenIds.Count != 0)
        {
            foreach (var allergenId in request.AllergenIds)
            {
                var allergen = await allergensRepository.GetByIdAsync(allergenId, cancellationToken);
                if (allergen == null)
                {
                    throw new NotFoundException($"Алерген з ID {allergenId} не знайдений");
                }
                position.Allergens.Add(allergen);
            }
        }

        if (request.ProductIds.Count == 0)
        {
            var id = await menuPositionRepository.AddAsync(position, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return id;
        }

        foreach (var productId in request.ProductIds)
        {
            var product = await productRepository.GetByIdAsync(productId, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException($"Продукт з ID {productId} не знайдений");
            }
            position.Products.Add(product);
        }

        var positionId = await menuPositionRepository.AddAsync(position, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return positionId;
    }
}