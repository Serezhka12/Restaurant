using FluentValidation;

namespace Application.Reservation.Commands.ReserveTable;

public class ReserveTableCommandValidator : AbstractValidator<ReserveTableCommand>
{
    public ReserveTableCommandValidator()
    {
        RuleFor(v => v.NumberOfPeople)
            .GreaterThan(0).WithMessage("Number of people must be greater than zero");

        RuleFor(v => v.ReservationDate)
            .GreaterThan(DateTime.Now).WithMessage("Reservation date must be in the future");
    }
} 