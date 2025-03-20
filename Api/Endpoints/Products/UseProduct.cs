using Application.Products.Commands.UseProduct;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Products;

namespace Api.Endpoints.Products;

internal sealed class UseProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Products.Use, async (int id, UseProductDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<UseProductCommand>(dto);
            command = command with { ProductId = id };

            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Products);
    }
}