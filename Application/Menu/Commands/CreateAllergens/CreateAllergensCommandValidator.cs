using FluentValidation;

namespace Application.Menu.Commands.CreateAllergens;

public class CreateAllergensCommandValidator: AbstractValidator<CreateAllergensCommand>
{
    public CreateAllergensCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}