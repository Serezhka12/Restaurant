using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Products.Commands.UseProduct;

public class UseProductCommandValidator : AbstractValidator<UseProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UseProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(v => v.ProductId)
            .GreaterThan(0)
            .MustAsync(ExistProduct).WithMessage("Product with specified ID does not exist");

        RuleFor(v => v.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");
    }

    private async Task<bool> ExistProduct(int id, CancellationToken cancellationToken)
    {
        return await _productRepository.ExistsAsync(id, cancellationToken);
    }
} 