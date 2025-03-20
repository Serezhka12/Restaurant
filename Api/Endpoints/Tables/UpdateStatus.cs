using Application.Tables.Commands.UpdateTableStatus;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Reservation;

namespace Api.Endpoints.Tables;

internal sealed class UpdateStatus : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch(Routes.Tables.Status, async (int id, UpdateTableStatusDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<UpdateTableStatusCommand>(dto);
            command = command with { Id = id };

            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Tables);
    }
}