using Application.Products.Queries.GetLowOnStockProducts;
using MediatR;

namespace Api.Endpoints.Products;

internal sealed class GetLowOnStock : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/low-on-stock", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var products = await sender.Send(new GetLowOnStockProductsQuery(), cancellationToken);
            return Results.Ok(products);
        }).WithTags(Tags.Products);
    }
} 