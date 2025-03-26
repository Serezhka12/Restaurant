using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.UpdateMenuPosition;

public record UpdateMenuPositionCommand(
    int Id,
    string Name,
    bool IsVegan,
    bool IsAvailable,
    List<int> AllergenIds,
    int MenuCategoryId,
    decimal Price,
    List<int> ProductIds) : IRequest;

public class UpdateMenuPositionCommandHandler(
    IMenuPositionRepository menuPositionRepository,
    IMenuCategoryRepository menuCategoryRepository,
    IAllergensRepository allergensRepository,
    IProductRepository productRepository,
    IApplicationDbContext dbContext)
    : IRequestHandler<UpdateMenuPositionCommand>
{
    public async Task Handle(UpdateMenuPositionCommand request, CancellationToken cancellationToken)
    {
        var position = await menuPositionRepository.GetByIdAsync(request.Id, cancellationToken);
        if (position == null)
        {
            throw new NotFoundException($"Позиція меню з ID {request.Id} не знайдена");
        }

        var categoryExists = await menuCategoryRepository.ExistsAsync(request.MenuCategoryId, cancellationToken);
        if (!categoryExists)
        {
            throw new NotFoundException($"Категорія меню з ID {request.MenuCategoryId} не знайдена");
        }

        position.Name = request.Name;
        position.IsVegan = request.IsVegan;
        position.IsAvailable = request.IsAvailable;
        position.Price = request.Price;
        position.MenuCategoryId = request.MenuCategoryId;


        position.Allergens.Clear();
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

        position.Products.Clear();
        if (request.ProductIds.Count != 0)
        {
            foreach (var productId in request.ProductIds)
            {
                var product = await productRepository.GetByIdAsync(productId, cancellationToken);
                if (product == null)
                {
                    throw new NotFoundException($"Продукт з ID {productId} не знайдений");
                }
                position.Products.Add(product);
            }
        }

        await menuPositionRepository.UpdateAsync(position, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}