using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Staff.Commands.UpdateStaff;

public class UpdateStaffCommandValidator : AbstractValidator<UpdateStaffCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateStaffCommandValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(v => v.Id)
            .GreaterThan(0)
            .MustAsync(ExistStaff).WithMessage("Співробітник з вказаним ID не існує");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Ім'я співробітника не може бути порожнім")
            .MaximumLength(100).WithMessage("Ім'я співробітника не може перевищувати 100 символів");

        RuleFor(v => v.Salary)
            .GreaterThan(0).WithMessage("Зарплата повинна бути більше нуля");

        RuleFor(v => v.WorkDays)
            .NotEmpty().WithMessage("Потрібно вказати хоча б один робочий день");
    }

    private async Task<bool> ExistStaff(int id, CancellationToken cancellationToken)
    {
        return await _employeeRepository.ExistsAsync(id, cancellationToken);
    }
} 