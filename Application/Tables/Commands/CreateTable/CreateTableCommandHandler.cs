using Application.Common.Interfaces;
using Domain.Entities.Reservation;
using MediatR;

namespace Application.Tables.Commands.CreateTable;

public record CreateTableCommand(
    int Seats,
    bool IsFree = true) : IRequest<int>;

public class CreateTableCommandHandler(ITableRepository tableRepository, IApplicationDbContext dbContext)
    : IRequestHandler<CreateTableCommand, int>
{
    public async Task<int> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var table = new Table
        {
            Seats = request.Seats,
            IsFree = request.IsFree,
        };

        var id = await tableRepository.AddAsync(table, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return id;
    }
}