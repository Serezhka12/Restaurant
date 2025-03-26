using Api.Dtos.Products;
using Application.Products.Commands.AddStorageItem;
using MapsterMapper;
using MediatR;

namespace Api.Endpoints.Products;

internal sealed class AddToStorage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Products.Storage, async (int id, AddStorageItemDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<AddStorageItemCommand>(dto);
            command = command with { ProductId = id };

            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Products);
    }
}