using Application.Menu.Contracts;
using MediatR;

namespace Application.Menu.Queries.GetMenuPositionsByCategory;

public record GetMenuPositionsByCategoryQuery : IRequest<List<MenuPositionGroupDto>>; 