using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(v => v.Id)
            .GreaterThan(0)
            .MustAsync(ExistProduct).WithMessage("Product with specified ID does not exist");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Product name cannot be empty")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

        RuleFor(v => v.MinimumQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum quantity cannot be negative");
    }

    private async Task<bool> ExistProduct(int id, CancellationToken cancellationToken)
    {
        return await _productRepository.ExistsAsync(id, cancellationToken);
    }
} 