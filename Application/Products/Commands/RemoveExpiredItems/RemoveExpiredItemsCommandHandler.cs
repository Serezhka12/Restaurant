using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Commands.RemoveExpiredItems;

public class RemoveExpiredItemsCommandHandler : IRequestHandler<RemoveExpiredItemsCommand>
{
    private readonly IProductRepository _productRepository;

    public RemoveExpiredItemsCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(RemoveExpiredItemsCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.RemoveExpiredItemsAsync(cancellationToken);
    }
} 