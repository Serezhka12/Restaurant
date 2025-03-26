using Application.Common.Interfaces;
using Application.Products.Contracts;
using MediatR;
using Shared.Exceptions;

namespace Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;

public class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException($"Product with ID {request.Id} not found");
        }

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            TotalQuantity = product.GetTotalQuantity(),
            AvailableQuantity = product.GetAvailableQuantity(),
            IsLowOnStock = product.IsLowOnStock(),
            StorageItems = product.StorageItems.Select(si =>
            {
                var storageItemDto = new StorageItemDto
                {
                    Id = si.Id,
                    Quantity = si.Quantity,
                    ReceivedDate = si.ReceivedDate,
                    ExpiryDate = si.ExpiryDate,
                    PurchasePrice = si.PurchasePrice,
                    IsExpired = si.IsExpired()
                };
                return storageItemDto;
            }).ToList()
        };

        return productDto;
    }
}