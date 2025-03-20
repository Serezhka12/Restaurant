using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Tables.Commands.UpdateTable;

public class UpdateTableCommandValidator : AbstractValidator<UpdateTableCommand>
{
    private readonly ITableRepository _tableRepository;

    public UpdateTableCommandValidator(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;

        RuleFor(v => v.Id)
            .GreaterThan(0)
            .MustAsync(ExistTable).WithMessage("Table with specified ID does not exist");

        RuleFor(v => v.Seats)
            .GreaterThan(0).WithMessage("Number of seats must be greater than zero");
    }

    private async Task<bool> ExistTable(int id, CancellationToken cancellationToken)
    {
        return await _tableRepository.ExistsAsync(id, cancellationToken);
    }
}