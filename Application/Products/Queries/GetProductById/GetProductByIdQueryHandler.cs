using Application.Common.Interfaces;
using Application.Products.Contracts;
using MediatR;
using Shared.Exceptions;


namespace Application.Products.Queries.GetProductById;

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
            TotalQuantity = productRepository.GetTotalQuantity(product),
            AvailableQuantity = productRepository.GetAvailableQuantity(product),
            IsLowOnStock = productRepository.IsLowOnStock(product),
            StorageItems = product.StorageItems.Select(si =>
            {
                var storageItemDto = new StorageItemDto
                {
                    IsExpired = productRepository.IsExpired(si)
                };
                return storageItemDto;
            }).ToList()
        };

        return productDto;
    }
}