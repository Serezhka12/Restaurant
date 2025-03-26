using Application.Common.Interfaces;
using Application.Reservation.Contracts;
using MediatR;
using Shared.Exceptions;

namespace Application.Reservation.Queries.GetReservationById;

public record GetReservationByIdQuery(int Id) : IRequest<ReservationDto>;

public class GetReservationByIdQueryHandler(IReservationRepository reservationRepository)
    : IRequestHandler<GetReservationByIdQuery, ReservationDto>
{
    public async Task<ReservationDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetByIdAsync(request.Id, cancellationToken);

        if (reservation == null)
        {
            throw new NotFoundException($"Reservation with ID {request.Id} not found");
        }

        return new ReservationDto
        {
            Id = reservation.Id,
            TableId = reservation.TableId,
            TableSeats = reservation.Table.Seats,
            ReservationDate = reservation.ReservationDate,
            NumberOfPeople = reservation.NumberOfPeople
        };
    }
} 