using Application.Products.Commands.UpdateProduct;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Products;
using Api.Extensions;

namespace Api.Endpoints.Products;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id}", async (int id, UpdateProductDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<UpdateProductCommand>(dto);
            command = command with { Id = id };
            
            await sender.Send(command, cancellationToken);
            return Results.Extensions.ApiResponse(new { Success = true }, 204);
        }).WithTags(Tags.Products);
    }
}