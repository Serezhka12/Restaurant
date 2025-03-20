using Application.Tables.Commands.CreateTable;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Reservation;

namespace Api.Endpoints.Tables;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Tables.Base, async (CreateTableDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<CreateTableCommand>(dto);
            var id = await sender.Send(command, cancellationToken);
            return Results.Created($"/{Routes.Tables.Base}/{id}", new { Id = id });
        }).WithTags(Tags.Tables);
    }
}