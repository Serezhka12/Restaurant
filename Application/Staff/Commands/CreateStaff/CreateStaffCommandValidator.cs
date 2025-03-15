using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Staff.Commands.CreateStaff;

public class CreateStaffCommandValidator : AbstractValidator<CreateStaffCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateStaffCommandValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Ім'я співробітника не може бути порожнім")
            .MaximumLength(100).WithMessage("Ім'я співробітника не може перевищувати 100 символів")
            .MustAsync(BeUniqueName).WithMessage("Співробітник з таким ім'ям вже існує");

        RuleFor(v => v.Salary)
            .GreaterThan(0).WithMessage("Зарплата повинна бути більше нуля");

        RuleFor(v => v.WorkDays)
            .NotEmpty().WithMessage("Потрібно вказати хоча б один робочий день");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _employeeRepository.ExistsByNameAsync(name, cancellationToken);
    }
} 