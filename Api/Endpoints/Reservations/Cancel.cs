using Application.Reservation.Commands.CancelReservation;
using MediatR;

namespace Api.Endpoints.Reservations;

internal sealed class Cancel : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Reservations.Cancel, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new CancelReservationCommand(id), cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Reservations);
    }
} 