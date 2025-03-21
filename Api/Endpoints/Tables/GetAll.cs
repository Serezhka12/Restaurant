using Application.Tables.Queries.GetAllTables;
using MediatR;

namespace Api.Endpoints.Tables;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Tables.Base, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var tables = await sender.Send(new GetAllTablesQuery(), cancellationToken);
            return Results.Ok(tables);
        }).WithTags(Tags.Tables);
    }
}