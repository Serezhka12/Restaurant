using Application.Tables.Commands.DeleteTable;
using MediatR;

namespace Api.Endpoints.Tables;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Tables.ById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteTableCommand(id), cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Tables);
    }
}