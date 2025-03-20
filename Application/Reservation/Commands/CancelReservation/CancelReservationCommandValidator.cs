using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Reservation.Commands.CancelReservation;

public class CancelReservationCommandValidator : AbstractValidator<CancelReservationCommand>
{
    private readonly IReservationRepository _reservationRepository;

    public CancelReservationCommandValidator(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;

        RuleFor(v => v.ReservationId)
            .GreaterThan(0)
            .MustAsync(ExistTable).WithMessage("Table with specified ID does not exist");
    }

    private async Task<bool> ExistTable(int id, CancellationToken cancellationToken)
    {
        var exist = await _reservationRepository.GetByIdAsync(id, cancellationToken);
        return exist != null;
    }
}