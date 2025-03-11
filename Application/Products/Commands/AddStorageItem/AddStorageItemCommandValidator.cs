using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Products.Commands.AddStorageItem;

public class AddStorageItemCommandValidator : AbstractValidator<AddStorageItemCommand>
{
    private readonly IProductRepository _productRepository;

    public AddStorageItemCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(v => v.ProductId)
            .GreaterThan(0)
            .MustAsync(ExistProduct).WithMessage("Product with specified ID does not exist");

        RuleFor(v => v.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");

        RuleFor(v => v.ExpiryDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiry date must be in the future");

        RuleFor(v => v.PurchasePrice)
            .GreaterThan(0).WithMessage("Purchase price must be greater than zero");
    }

    private async Task<bool> ExistProduct(int id, CancellationToken cancellationToken)
    {
        return await _productRepository.ExistsAsync(id, cancellationToken);
    }
} 