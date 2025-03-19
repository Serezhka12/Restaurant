using Application.Products.Commands.DeleteProduct;
using MediatR;

namespace Api.Endpoints.Products;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Products.ById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteProductCommand(id), cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Products);
    }
} 