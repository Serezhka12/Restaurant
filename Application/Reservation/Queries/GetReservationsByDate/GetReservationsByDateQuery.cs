using Application.Reservation.Contracts;
using MediatR;

namespace Application.Reservation.Queries.GetReservationsByDate;

public record GetReservationsByDateQuery(DateTime Date) : IRequest<List<ReservationDto>>; 