using Application.Common.Interfaces;
using Domain.Entities.Products;
using MediatR;
using Shared.Exceptions;

namespace Application.Products.Commands.AddStorageItem;

public class AddStorageItemCommandHandler(IProductRepository productRepository, IApplicationDbContext dbContext)
    : IRequestHandler<AddStorageItemCommand>
{
    public async Task Handle(AddStorageItemCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException($"Product with ID {request.ProductId} not found");
        }

        var storageItem = new StorageItem
        {
            ProductId = product.Id,
            Quantity = request.Quantity,
            ReceivedDate = DateTime.UtcNow,
            ExpiryDate = request.ExpiryDate,
            PurchasePrice = request.PurchasePrice
        };

        await dbContext.StorageItems.AddAsync(storageItem, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}