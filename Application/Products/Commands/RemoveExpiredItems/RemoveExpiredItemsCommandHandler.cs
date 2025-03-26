using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.RemoveExpiredItems;

public record RemoveExpiredItemsCommand : IRequest;

public class RemoveExpiredItemsCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<RemoveExpiredItemsCommand>
{
    public async Task Handle(RemoveExpiredItemsCommand request, CancellationToken cancellationToken)
    {
        var expiredItems = await dbContext.StorageItems
            .Where(si => si.ExpiryDate < DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        dbContext.StorageItems.RemoveRange(expiredItems);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}