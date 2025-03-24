using Application.Menu.Contracts;
using MediatR;

namespace Application.Menu.Queries.GetMenuCategoryById;

public record GetMenuCategoryByIdQuery(int Id) : IRequest<MenuCategoryDto>; 