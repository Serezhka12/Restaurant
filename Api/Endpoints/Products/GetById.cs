using Application.Products.Queries.GetProductById;
using MediatR;

namespace Api.Endpoints.Products;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var product = await sender.Send(new GetProductByIdQuery(id), cancellationToken);
            return Results.Ok(product);
        }).WithTags(Tags.Products);
    }
}