using FluentValidation;

namespace Application.Tables.Commands.CreateTable;

public class CreateTableCommandValidator : AbstractValidator<CreateTableCommand>
{
    public CreateTableCommandValidator()
    {
        RuleFor(v => v.Seats)
            .GreaterThan(0).WithMessage("Number of seats must be greater than zero");
    }
}