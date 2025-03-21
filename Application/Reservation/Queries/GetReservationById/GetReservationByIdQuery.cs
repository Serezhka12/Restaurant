using Application.Reservation.Contracts;
using MediatR;

namespace Application.Reservation.Queries.GetReservationById;

public record GetReservationByIdQuery(int Id) : IRequest<ReservationDto>; 