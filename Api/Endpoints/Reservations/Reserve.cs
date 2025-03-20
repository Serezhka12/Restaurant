using Application.Reservation.Commands.ReserveTable;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Reservation;

namespace Api.Endpoints.Reservations;

internal sealed class Reserve : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Reservations.Reserve, async (ReserveTableDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<ReserveTableCommand>(dto);
            var reservationId = await sender.Send(command, cancellationToken);
            return Results.Created($"/{Routes.Reservations.Base}/{reservationId}", new { Id = reservationId });
        }).WithTags(Tags.Reservations);
    }
}