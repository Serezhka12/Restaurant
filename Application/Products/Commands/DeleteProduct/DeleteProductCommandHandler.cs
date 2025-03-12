using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await productRepository.DeleteAsync(request.Id, cancellationToken);
    }
}