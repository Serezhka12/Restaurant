using Application.Products.Commands.CreateProduct;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Products;
using Api.Extensions;

namespace Api.Endpoints.Products;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (CreateProductDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<CreateProductCommand>(dto);
            var id = await sender.Send(command, cancellationToken);
            return Results.Extensions.ApiResponse(new { Id = id }, 201);
        }).WithTags(Tags.Products);
    }
}