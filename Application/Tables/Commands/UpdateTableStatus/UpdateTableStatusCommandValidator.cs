using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Tables.Commands.UpdateTableStatus;

public class UpdateTableStatusCommandValidator : AbstractValidator<UpdateTableStatusCommand>
{
    private readonly ITableRepository _tableRepository;

    public UpdateTableStatusCommandValidator(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;

        RuleFor(v => v.Id)
            .GreaterThan(0)
            .MustAsync(ExistTable).WithMessage("Table with specified ID does not exist");
    }

    private async Task<bool> ExistTable(int id, CancellationToken cancellationToken)
    {
        return await _tableRepository.ExistsAsync(id, cancellationToken);
    }
}