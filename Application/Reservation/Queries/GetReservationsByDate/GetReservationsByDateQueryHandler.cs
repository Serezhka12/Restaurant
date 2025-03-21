using Application.Common.Interfaces;
using Application.Reservation.Contracts;
using MediatR;

namespace Application.Reservation.Queries.GetReservationsByDate;

public class GetReservationsByDateQueryHandler(IReservationRepository reservationRepository)
    : IRequestHandler<GetReservationsByDateQuery, List<ReservationDto>>
{
    public async Task<List<ReservationDto>> Handle(GetReservationsByDateQuery request, CancellationToken cancellationToken)
    {
        var reservations = await reservationRepository.GetByDateAsync(request.Date, cancellationToken);

        return reservations.Select(r => new ReservationDto
        {
            Id = r.Id,
            TableId = r.TableId,
            TableSeats = r.Table.Seats,
            ReservationDate = r.ReservationDate,
            NumberOfPeople = r.NumberOfPeople
        }).ToList();
    }
} 