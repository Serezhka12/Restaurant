using Application.Products.Commands.CreateProduct;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Products;

namespace Api.Endpoints.Products;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Products.Base, async (CreateProductDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<CreateProductCommand>(dto);
            var id = await sender.Send(command, cancellationToken);
            return Results.Created($"/{Routes.Products.Base}/{id}", new { Id = id });
        }).WithTags(Tags.Products);
    }
}