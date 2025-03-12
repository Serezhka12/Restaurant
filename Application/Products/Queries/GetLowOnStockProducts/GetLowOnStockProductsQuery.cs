using Application.Products.Contracts;
using MediatR;


namespace Application.Products.Queries.GetLowOnStockProducts;

public record GetLowOnStockProductsQuery : IRequest<List<ProductDto>>;