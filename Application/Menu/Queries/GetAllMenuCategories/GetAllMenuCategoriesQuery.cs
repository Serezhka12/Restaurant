using Application.Menu.Contracts;
using MediatR;

namespace Application.Menu.Queries.GetAllMenuCategories;

public record GetAllMenuCategoriesQuery : IRequest<List<MenuCategoryDto>>; 