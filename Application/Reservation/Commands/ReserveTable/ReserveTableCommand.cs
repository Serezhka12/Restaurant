using MediatR;

namespace Application.Reservation.Commands.ReserveTable;

public record ReserveTableCommand(
    int NumberOfPeople,
    DateTime ReservationDate) : IRequest<int>; 