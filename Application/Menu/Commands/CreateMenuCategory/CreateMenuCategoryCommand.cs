using MediatR;

namespace Application.Menu.Commands.CreateMenuCategory;

public record CreateMenuCategoryCommand(
    string Name,
    bool IsAvailable) : IRequest<int>; 