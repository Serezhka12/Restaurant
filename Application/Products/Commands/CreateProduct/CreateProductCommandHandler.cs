using Application.Common.Interfaces;
using Domain.Entities.Products;
using MediatR;

namespace Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Unit = request.Unit,
            MinimumQuantity = request.MinimumQuantity
        };

        return await _productRepository.AddAsync(product, cancellationToken);
    }
}