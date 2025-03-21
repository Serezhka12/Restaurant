using Application.Reservation.Queries.GetReservationsByDate;
using MediatR;

namespace Api.Endpoints.Reservations;

internal sealed class GetByDate : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Reservations.ByDate, async (DateTime date, ISender sender, CancellationToken cancellationToken) =>
        {
            var reservations = await sender.Send(new GetReservationsByDateQuery(date), cancellationToken);
            return Results.Ok(reservations);
        }).WithTags(Tags.Reservations);
    }
} 