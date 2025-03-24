using MediatR;

namespace Application.Menu.Commands.UpdateMenuCategory;

public record UpdateMenuCategoryCommand(
    int Id,
    string Name,
    bool IsAvailable) : IRequest; 