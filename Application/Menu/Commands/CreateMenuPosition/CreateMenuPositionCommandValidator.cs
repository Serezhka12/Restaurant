using FluentValidation;

namespace Application.Menu.Commands.CreateMenuPosition;

public class CreateMenuPositionCommandValidator: AbstractValidator<CreateMenuPositionCommand>
{
    public CreateMenuPositionCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
    }
}