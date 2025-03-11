using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Product with ID {request.Id} not found");
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.MinimumQuantity = request.MinimumQuantity;

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}