using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Product name cannot be empty")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters")
            .MustAsync(BeUniqueName).WithMessage("Product with this name already exists");

        RuleFor(v => v.Unit)
            .NotEmpty().WithMessage("Unit of measurement cannot be empty")
            .MaximumLength(20).WithMessage("Unit of measurement cannot exceed 20 characters");

        RuleFor(v => v.MinimumQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum quantity cannot be negative");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _productRepository.ExistsByNameAsync(name, cancellationToken);
    }
} 