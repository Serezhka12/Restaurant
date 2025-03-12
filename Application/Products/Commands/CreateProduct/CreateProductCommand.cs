using MediatR;

namespace Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    string Unit,
    decimal MinimumQuantity) : IRequest<int>; 