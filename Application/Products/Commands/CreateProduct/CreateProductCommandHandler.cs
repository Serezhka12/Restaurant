using Application.Common.Interfaces;
using Domain.Entities.Products;
using MediatR;

namespace Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    string Unit,
    decimal MinimumQuantity) : IRequest<int>;

public class CreateProductCommandHandler(IProductRepository productRepository, IApplicationDbContext dbContext)
    : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Unit = request.Unit,
            MinimumQuantity = request.MinimumQuantity
        };

        var id = await productRepository.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return id;
    }
}