using Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace Application.Reservation.Commands.CancelReservation;

public class CancelReservationCommandHandler(IReservationRepository reservationRepository)
    : IRequestHandler<CancelReservationCommand>
{
    public async Task Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken);

        if (reservation == null)
        {
            throw new NotFoundException($"Reservation with ID {request.ReservationId} not found");
        }

        await reservationRepository.DeleteAsync(request.ReservationId, cancellationToken);
    }
} 