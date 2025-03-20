using Application.Tables.Queries.GetTableById;
using MediatR;

namespace Api.Endpoints.Tables;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Tables.ById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var table = await sender.Send(new GetTableByIdQuery(id), cancellationToken);
            return Results.Ok(table);
        }).WithTags(Tags.Tables);
    }
}