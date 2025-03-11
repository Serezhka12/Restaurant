using Application.Common.Interfaces;
using Application.Products.Contracts;
using MediatR;

namespace Application.Products.Queries.GetLowOnStockProducts;

public class GetLowOnStockProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetLowOnStockProductsQuery, List<ProductDto>>
{
    public async Task<List<ProductDto>> Handle(GetLowOnStockProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetLowOnStockAsync(cancellationToken);

        return products.Select(product =>
        {
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
        }).ToList();
    }
}