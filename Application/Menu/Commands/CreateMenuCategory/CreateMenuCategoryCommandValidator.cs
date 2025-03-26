using FluentValidation;

namespace Application.Menu.Commands.CreateMenuCategory;

public class CreateMenuCategoryCommandValidator: AbstractValidator<CreateMenuCategoryCommand>
{
    public CreateMenuCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}