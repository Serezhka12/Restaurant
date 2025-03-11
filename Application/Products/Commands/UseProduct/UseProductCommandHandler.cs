using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Commands.UseProduct;

public class UseProductCommandHandler : IRequestHandler<UseProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UseProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UseProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Product with ID {request.ProductId} not found");
        }

        await _productRepository.UseProductAsync(product, request.Quantity, cancellationToken);
    }
}