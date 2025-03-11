using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Commands.AddStorageItem;

public class AddStorageItemCommandHandler : IRequestHandler<AddStorageItemCommand>
{
    private readonly IProductRepository _productRepository;

    public AddStorageItemCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(AddStorageItemCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Product with ID {request.ProductId} not found");
        }

        await _productRepository.AddToStorageAsync(product, request.Quantity, request.ExpiryDate, request.PurchasePrice, cancellationToken);
    }
}