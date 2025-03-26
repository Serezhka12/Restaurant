using Application.Common.Interfaces;
using Application.Tables.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tables.Queries.GetAllTables;

public record GetAllTablesQuery : IRequest<List<TableDto>>;

public class GetAllTablesQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetAllTablesQuery, List<TableDto>>
{
    public async Task<List<TableDto>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
    {
        var tables = await dbContext.Tables.ToListAsync(cancellationToken);

        return tables.Select(table => new TableDto
        {
            Id = table.Id,
            Seats = table.Seats,
            IsFree = table.IsFree,
        }).ToList();
    }
}