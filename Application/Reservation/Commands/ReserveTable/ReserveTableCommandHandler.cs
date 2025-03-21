using Application.Common.Interfaces;
using Domain.Entities.Reservation;
using MediatR;
using Shared.Exceptions;

namespace Application.Reservation.Commands.ReserveTable;

public class ReserveTableCommandHandler(IReservationRepository reservationRepository)
    : IRequestHandler<ReserveTableCommand, int>
{
    public async Task<int> Handle(ReserveTableCommand request, CancellationToken cancellationToken)
    {
        var availableTables = await reservationRepository.GetAvailableTablesAsync(
            request.NumberOfPeople,
            request.ReservationDate,
            cancellationToken);

        if (availableTables.Count == 0)
        {
            throw new InvalidOperationException($"No available tables for {request.NumberOfPeople} people on {request.ReservationDate.ToShortDateString()}");
        }

        var optimalTable = availableTables
            .OrderBy(t => t.Seats - request.NumberOfPeople)
            .First();

        var reservation = new TableReservation
        {
            TableId = optimalTable.Id,
            ReservationDate = request.ReservationDate,
            NumberOfPeople = request.NumberOfPeople
        };

        return await reservationRepository.AddAsync(reservation, cancellationToken);
    }
}