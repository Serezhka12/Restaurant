using Application.Products.Commands.AddStorageItem;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Products;
using Api.Extensions;

namespace Api.Endpoints.Products;

internal sealed class AddToStorage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products/{id}/storage", async (int id, AddStorageItemDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<AddStorageItemCommand>(dto);
            command = command with { ProductId = id };
            
            await sender.Send(command, cancellationToken);
            return Results.Extensions.ApiResponse(new { Success = true }, 204);
        }).WithTags(Tags.Products);
    }
}