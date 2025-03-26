using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace Application.Products.Commands.UseProduct;

public record UseProductCommand(
    int ProductId,
    decimal Quantity) : IRequest;

public class UseProductCommandHandler(IProductRepository productRepository, IApplicationDbContext dbContext)
    : IRequestHandler<UseProductCommand>
{
    public async Task Handle(UseProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException($"Product with ID {request.ProductId} not found");
        }

        var availableItems = product.StorageItems
            .Where(si => !si.IsExpired() && si.Quantity > 0)
            .OrderBy(si => si.ExpiryDate)
            .ToList();

        var remainingQuantity = request.Quantity;

        foreach (var item in availableItems.TakeWhile(_ => remainingQuantity > 0))
        {
            if (item.Quantity <= remainingQuantity)
            {
                remainingQuantity -= item.Quantity;
                item.Quantity = 0;
            }
            else
            {
                item.Quantity -= remainingQuantity;
                remainingQuantity = 0;
            }
        }

        if (remainingQuantity > 0)
        {
            throw new InvalidOperationException($"Insufficient product in storage. Missing {remainingQuantity} {product.Unit}");
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}