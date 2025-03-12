using Application.Products.Queries.GetAllProducts;
using MediatR;

namespace Api.Endpoints.Products;

internal sealed class GetAll: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var products = await sender.Send(new GetAllProductsQuery(), cancellationToken);
            return Results.Ok(products);
        }).WithTags(Tags.Products);
    }
}