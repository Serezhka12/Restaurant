using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Product with ID {request.Id} not found");
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.MinimumQuantity = request.MinimumQuantity;

        await productRepository.UpdateAsync(product, cancellationToken);
    }
}