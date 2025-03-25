using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.UpdateMenuPosition;

public class UpdateMenuPositionCommandHandler : IRequestHandler<UpdateMenuPositionCommand>
{
    private readonly IMenuPositionRepository _menuPositionRepository;
    private readonly IMenuCategoryRepository _menuCategoryRepository;
    private readonly IAllergensRepository _allergensRepository;
    private readonly IProductRepository _productRepository;

    public UpdateMenuPositionCommandHandler(
        IMenuPositionRepository menuPositionRepository,
        IMenuCategoryRepository menuCategoryRepository,
        IAllergensRepository allergensRepository,
        IProductRepository productRepository)
    {
        _menuPositionRepository = menuPositionRepository;
        _menuCategoryRepository = menuCategoryRepository;
        _allergensRepository = allergensRepository;
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateMenuPositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _menuPositionRepository.GetByIdAsync(request.Id, cancellationToken);
        if (position == null)
        {
            throw new NotFoundException($"Позиція меню з ID {request.Id} не знайдена");
        }

        var categoryExists = await _menuCategoryRepository.ExistsAsync(request.MenuCategoryId, cancellationToken);
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
                var allergen = await _allergensRepository.GetByIdAsync(allergenId, cancellationToken);
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
                var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
                if (product == null)
                {
                    throw new NotFoundException($"Продукт з ID {productId} не знайдений");
                }
                position.Products.Add(product);
            }
        }

        await _menuPositionRepository.UpdateAsync(position, cancellationToken);
    }
}