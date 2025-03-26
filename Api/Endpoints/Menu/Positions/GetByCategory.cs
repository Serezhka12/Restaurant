using Application.Menu.Queries.GetMenuPositionsByCategory;
using MediatR;

namespace Api.Endpoints.Menu.Positions;

internal sealed class GetByCategory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Menu.GetMenu, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var positionsByCategory = await sender.Send(new GetMenuPositionsByCategoryQuery(), cancellationToken);
            return Results.Ok(positionsByCategory);
        }).WithTags(Tags.Menu);
    }
}