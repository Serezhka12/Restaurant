using Application.Common.Interfaces;
using Application.Tables.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace Application.Tables.Queries.GetTableById;

public record GetTableByIdQuery(int Id) : IRequest<TableDto>;

public class GetTableByIdQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetTableByIdQuery, TableDto>
{
    public async Task<TableDto> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
    {
        var table = await dbContext.Tables
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (table == null)
        {
            throw new NotFoundException($"Table with ID {request.Id} not found");
        }

        return new TableDto
        {
            Id = table.Id,
            Seats = table.Seats,
            IsFree = table.IsFree,
        };
    }
}