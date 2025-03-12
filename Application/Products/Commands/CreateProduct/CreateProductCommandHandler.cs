using Application.Common.Interfaces;
using Domain.Entities.Products;
using MediatR;

namespace Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Unit = request.Unit,
            MinimumQuantity = request.MinimumQuantity
        };

        return await productRepository.AddAsync(product, cancellationToken);
    }
}