using Application.Menu.Queries.GetAllMenuPositions;
using MediatR;

namespace Api.Endpoints.Menu.Positions;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Menu.Positions, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var positions = await sender.Send(new GetAllMenuPositionsQuery(), cancellationToken);
            return Results.Ok(positions);
        }).WithTags(Tags.Menu);
    }
} 