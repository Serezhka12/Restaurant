using Application.Products.Commands.DeleteProduct;
using MediatR;
using Api.Extensions;

namespace Api.Endpoints.Products;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteProductCommand(id), cancellationToken);
            return Results.Extensions.ApiResponse(new { Success = true }, 204);
        }).WithTags(Tags.Products);
    }
} 