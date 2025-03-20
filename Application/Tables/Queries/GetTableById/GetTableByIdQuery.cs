using Application.Tables.Contracts;
using MediatR;

namespace Application.Tables.Queries.GetTableById;

public record GetTableByIdQuery(int Id) : IRequest<TableDto>;