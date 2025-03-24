using Api.Dtos.Tables;
using Application.Tables.Commands.UpdateTable;
using MapsterMapper;
using MediatR;

namespace Api.Endpoints.Tables;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Tables.ById, async (int id, UpdateTableDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<UpdateTableCommand>(dto);
            command = command with { Id = id };

            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Tables);
    }
}