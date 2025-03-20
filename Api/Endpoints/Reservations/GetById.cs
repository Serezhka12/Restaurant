using Application.Reservation.Queries.GetReservationById;
using MediatR;

namespace Api.Endpoints.Reservations;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Reservations.ById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var reservation = await sender.Send(new GetReservationByIdQuery(id), cancellationToken);
            return Results.Ok(reservation);
        }).WithTags(Tags.Reservations);
    }
} 