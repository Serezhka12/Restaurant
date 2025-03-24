using MediatR;

namespace Application.Menu.Commands.DeleteMenuCategory;

public record DeleteMenuCategoryCommand(int Id) : IRequest; 