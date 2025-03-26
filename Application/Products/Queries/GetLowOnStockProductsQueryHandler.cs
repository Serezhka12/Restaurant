using Application.Common.Interfaces;
using Application.Products.Contracts;
using MediatR;

namespace Application.Products.Queries.GetLowOnStockProducts;

public record GetLowOnStockProductsQuery : IRequest<List<ProductDto>>;

public class GetLowOnStockProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetLowOnStockProductsQuery, List<ProductDto>>
{
    public async Task<List<ProductDto>> Handle(GetLowOnStockProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);
        var lowOnStockProducts = products.Where(p => p.IsLowOnStock()).ToList();

        return lowOnStockProducts.Select(product =>
        {
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
        }).ToList();
    }
}