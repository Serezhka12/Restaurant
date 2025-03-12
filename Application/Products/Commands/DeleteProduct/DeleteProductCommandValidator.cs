using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(v => v.Id)
            .GreaterThan(0)
            .MustAsync(ExistProduct).WithMessage("Product with specified ID does not exist");
    }

    private async Task<bool> ExistProduct(int id, CancellationToken cancellationToken)
    {
        return await _productRepository.ExistsAsync(id, cancellationToken);
    }
} 