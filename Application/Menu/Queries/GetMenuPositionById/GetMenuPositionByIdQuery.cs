using Application.Menu.Contracts;
using MediatR;

namespace Application.Menu.Queries.GetMenuPositionById;

public record GetMenuPositionByIdQuery(int Id) : IRequest<MenuPositionDto>; 