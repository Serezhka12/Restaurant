using Application.Products.Contracts;
using MediatR;


namespace Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<List<ProductDto>>;