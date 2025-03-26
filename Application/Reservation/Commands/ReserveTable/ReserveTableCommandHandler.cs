using Application.Common.Interfaces;
using Domain.Entities.Reservation;
using MediatR;
using Shared.Exceptions;

namespace Application.Reservation.Commands.ReserveTable;

public record ReserveTableCommand(
    int NumberOfPeople,
    DateTime ReservationDate) : IRequest<int>;

public class ReserveTableCommandHandler(IReservationRepository reservationRepository, IApplicationDbContext dbContext)
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

        var id = await reservationRepository.AddAsync(reservation, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return id;
    }
}