using Application.Common.Interfaces;
using Domain.Entities.Reservation;
using MediatR;

namespace Application.Tables.Commands.CreateTable;

public class CreateTableCommandHandler(ITableRepository tableRepository)
    : IRequestHandler<CreateTableCommand, int>
{
    public async Task<int> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var table = new Table
        {
            Seats = request.Seats,
            IsFree = request.IsFree,
        };

        return await tableRepository.AddAsync(table, cancellationToken);
    }
}