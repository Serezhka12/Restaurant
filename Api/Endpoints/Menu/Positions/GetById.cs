using Application.Menu.Queries.GetMenuPositionById;
using MediatR;

namespace Api.Endpoints.Menu.Positions;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Menu.PositionById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var position = await sender.Send(new GetMenuPositionByIdQuery(id), cancellationToken);
            return Results.Ok(position);
        }).WithTags(Tags.Menu);
    }
} 