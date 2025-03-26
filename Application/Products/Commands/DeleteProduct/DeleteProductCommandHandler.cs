using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler(IProductRepository productRepository, IApplicationDbContext dbContext) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await productRepository.DeleteAsync(request.Id, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}