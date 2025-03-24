using Application.Common.Interfaces;
using Domain.Entities.Menu;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Commands.CreateMenuPosition;

public class CreateMenuPositionCommandHandler : IRequestHandler<CreateMenuPositionCommand, int>
{
    private readonly IMenuPositionRepository _menuPositionRepository;
    private readonly IMenuCategoryRepository _menuCategoryRepository;
    private readonly IAllergensRepository _allergensRepository;
    private readonly IProductRepository _productRepository;

    public CreateMenuPositionCommandHandler(
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

    public async Task<int> Handle(CreateMenuPositionCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _menuCategoryRepository.ExistsAsync(request.MenuCategoryId, cancellationToken);
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
            Allergens = new List<Allergens>(),
            Products = new List<Domain.Entities.Products.Product>()
        };

        // Додаємо алергени
        if (request.AllergenIds.Any())
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

        // Додаємо продукти
        if (request.ProductIds.Any())
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

        return await _menuPositionRepository.AddAsync(position, cancellationToken);
    }
} 