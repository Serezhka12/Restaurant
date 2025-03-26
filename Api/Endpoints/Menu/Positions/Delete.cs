using Application.Menu.Commands.DeleteMenuPosition;
using MediatR;

namespace Api.Endpoints.Menu.Positions;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Menu.PositionById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteMenuPositionCommand(id), cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Menu);
    }
} 