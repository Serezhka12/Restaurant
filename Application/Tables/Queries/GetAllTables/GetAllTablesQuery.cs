using Application.Tables.Contracts;
using MediatR;

namespace Application.Tables.Queries.GetAllTables;

public record GetAllTablesQuery : IRequest<List<TableDto>>;