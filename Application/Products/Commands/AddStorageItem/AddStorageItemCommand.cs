using MediatR;

namespace Application.Products.Commands.AddStorageItem;

public record AddStorageItemCommand(
    int ProductId,
    decimal Quantity,
    DateTime ExpiryDate,
    decimal PurchasePrice) : IRequest; 