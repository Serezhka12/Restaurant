using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Staff.Commands.DeleteStaff;

public class DeleteStaffCommandValidator : AbstractValidator<DeleteStaffCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteStaffCommandValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(v => v.Id)
            .GreaterThan(0)
            .MustAsync(ExistStaff).WithMessage("Співробітник з вказаним ID не існує");
    }

    private async Task<bool> ExistStaff(int id, CancellationToken cancellationToken)
    {
        return await _employeeRepository.ExistsAsync(id, cancellationToken);
    }
} 