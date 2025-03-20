using MediatR;

namespace Application.Reservation.Commands.CancelReservation;

public record CancelReservationCommand(int ReservationId) : IRequest; 