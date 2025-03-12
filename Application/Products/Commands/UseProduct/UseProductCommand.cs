using MediatR;

namespace Application.Products.Commands.UseProduct;

public record UseProductCommand(
    int ProductId,
    decimal Quantity) : IRequest; 