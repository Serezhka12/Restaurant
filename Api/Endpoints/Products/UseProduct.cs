using Application.Products.Commands.UseProduct;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Products;
using Api.Extensions;

namespace Api.Endpoints.Products;

internal sealed class UseProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products/{id}/use", async (int id, UseProductDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<UseProductCommand>(dto);
            command = command with { ProductId = id };
            
            await sender.Send(command, cancellationToken);
            return Results.Extensions.ApiResponse(new { Success = true }, 204);
        }).WithTags(Tags.Products);
    }
}