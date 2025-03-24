using Application.Menu.Contracts;
using MediatR;

namespace Application.Menu.Queries.GetAllMenuPositions;

public record GetAllMenuPositionsQuery : IRequest<List<MenuPositionDto>>; 