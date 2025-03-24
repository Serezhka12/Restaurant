using Api.Dtos.Menu;
using Application.Menu.Commands.UpdateMenuPosition;
using MapsterMapper;
using MediatR;

namespace Api.Endpoints.Menu.Positions;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Menu.PositionById, async (int id, UpdateMenuPositionDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<UpdateMenuPositionCommand>(dto) with { Id = id };
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Menu);
    }
} 